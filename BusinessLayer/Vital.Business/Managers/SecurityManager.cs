using System;
using System.Text;
using System.Runtime.InteropServices;
using Vital.Business.Shared;
using Vital.Business.Shared.Exceptions;
using Vital.Business.Shared.Shared;

namespace Vital.Business.Managers
{
    public class SecurityManager : BaseManager
    {
        #region KEY-LOK Dongle API's

        /// <summary>
        /// Platform invoke declaration for keylok interface KFUNC function.
        /// </summary>
        // ReSharper disable UnusedMember.Local
        // ReSharper disable InconsistentNaming
        [DllImport("KL2DLL32.DLL", CharSet = CharSet.Auto)]
        private static extern uint KFUNC(int arg1, int arg2, int arg3, int arg4);
        [DllImport("KL2DLL32.DLL", CharSet = CharSet.Auto)]
        private static extern uint KEYBD(int arg1);
        [DllImport("KL2DLL32.DLL", CharSet = CharSet.Ansi)]
        private static extern uint KBLOCK(uint task, uint address, uint byteCount, ushort[] pArray);
        [DllImport("KL2DLL32.DLL", CharSet = CharSet.Ansi)]
        private static extern uint KEXEC(string ExeDir, string ExeFile, string UserPin, StringBuilder InBuffer, short SizeBuffer);
        [DllImport("KL2DLL32.DLL", CharSet = CharSet.Ansi)]
        public static extern void KGETGUSN(IntPtr pArray);
        [DllImport("KL2DLL32.DLL", CharSet = CharSet.Ansi)]
        public static extern ulong GETLASTKEYERROR();
        // ReSharper restore UnusedMember.Local
        // ReSharper restore InconsistentNaming
        #endregion

        #region Private Variables

        private int _expirationDay;
        private int _expirationMonth;
        private int _expirationYear;

        #endregion

        #region Properties

        public DateTime ExpirationDate
        {
            get
            {
                return new DateTime(_expirationYear, _expirationMonth, _expirationDay);
            }
        }

        #endregion

        #region Constants

        #region Messages

        public static string LeaseExpiredMessage = "The lease associated with the use of this software has expired. The Dongle must be reprogrammed for continued use.";
        public static string SysDateSetBackMessage = "WARNING: The system clock has been set back to an earlier date.";
        public static string NoLeaseDateMessage = "The dongle has not been programmed with a lease expiration date.";
        public static string LeaseDateBadMessage = "The programmed lease date is in the past.  Please reprogram with a future date.";
        public static string LastSysDateCorruptMessage = "The 'last system date' stored in the KEY-LOK is corrupt.";
        public static string DefaultMessage = "";
        public static string NoKeyLoKMessage = "Please make sure the dongle is attached or attach the right one if you have multiple types.";
        public static string KeyLokStateTitle = "Dongle Status";
        public static string RemoteTaskSimulation = "END USER - REMOTE TASK SIMULATION";
        public static string CallDeveloperOne = "Please call your software developer at (xxx) xxx-xxxx. The following 5 codes must be provided to the developer in order to receive your special activation code sequence.";
        public static string CallDeveloperTwo = "Once you confirm that your software supplier has input the codes correctly you can click on 'OK' to process with the remote task.";
        public static string DongleLeaseExtended = "Dongle lease date has been extended.  Please re-enter the Vital Program";
        public static string DongleError = "ERROR: Requested task was not performed";
        public static string DongleNotCompitable = "This dongle attached has not been configured to run Vital, please check with support to make sure it has been licensed.";

        #endregion

        #region Logic Contsants

        private const int Portoption = 0;

        #endregion

        #region Company Codes
        // ReSharper disable InconsistentNaming
        private const ushort ValidateCode1 = 0X1996;	// These three codes passed to the dongle as the first part
        private const ushort ValidateCode2 = 0X9152;	// of its authentication sequence.
        private const ushort ValidateCode3 = 0XC8CF;
        private const ushort ClientIDCode1 = 0X68ED;	// These values are returned by the dongle as the result of the authentication 
        private const ushort ClientIDCode2 = 0X4350;	// sequence. We should check that the returned value matches these values.        
        private const ushort ReadCode1 = 0XA23;
        private const ushort ReadCode2 = 0X7489;
        private const ushort ReadCode3 = 0XE3DF;
        private const ushort WriteCode1 = 0X80CF;		// These values must be passed to the dongle to authorize write operations
        private const ushort WriteCode2 = 0X21D8;		// to its non-volatile RAM.
        private const ushort WriteCode3 = 0X550C;
        private static int ReturnValue1;
        private static int ReturnValue2;
        // ReSharper restore InconsistentNaming
        #endregion

        #region Operation Codes

        /// <summary>
        /// The following function (operation) codes are used as the first argument to the KFUNC() interface function in the DLL
        /// to specify the desired operation to perform.
        /// </summary>
        private const ushort KlCheck = 1;
        private const ushort GetSN = 3;
        private const ushort GetVarWord = 4;
        private const ushort READAUTH = 2;
        private const ushort WRITEAUTH = 5;
        private const ushort REMOTEUPDUPT1 = 13;
        private const ushort REMOTEUPDUPT2 = 14;
        private const ushort REMOTEUPDUPT3 = 15;
        private const ushort RemoteDateExtend = 1;
        private const ushort Remotegetmemory = 4;
        private const ushort Remotereplace = 3;
        private static int SETEXPDATE = 10;

        public const long KeyErrorNoerror = 0;
        public const long KeyErrorFortressNoauthentication = 536871681;  //KEYLOK Authentication has not been performed
        public const long KeyErrorFortressNofolder = 536871682;          //Client subfolder with desired program not found
        public const long KeyErrorFortressWrongpin = 536871683;          //Client subfolder with desired program not found
        public const long KeyErrorExeError = 536871684;                  //Client EXE Error
        public const long KeyErrorNoRealclock = 536870936;               // No RTC on board
        public const long KeyErrorRtcNoPower = 536870937;               // RTC has been powered down (battery has lost power)

        #endregion

        #region Lease expiration constants

        const int Leaseexpired = 65534;
        const int Sysdatesetback = 65533;
        const int Noleasedate = 65532;
        const int Leasedatebad = 65531;
        const int Lastsysdatecorrupt = 65530;
        const int Ckleasedate = 9;
        public const String Title = "LEASE DATE CHECK";
        const int Getexpdate = 8;
        const int BaseYear = 1990;

        #endregion

        #endregion

        #region Logic

        #region Dongle Basic Logic

        /// <summary>
        /// Authorizes read operations on the dongle's non-volatile RAM. This must be called after
        /// calling IsPresent() which resets the dongle's internal read authorization state.
        /// </summary>
        /// <returns></returns>
        public static void AuthorizeRead()
        {
            KFUNC(READAUTH, ReadCode1, ReadCode2, ReadCode3);
        }
        
        /// <summary>
        /// Authorizes write operations on the dongle's non-volatile RAM. This must be called after
        /// calling IsPresent() which resets the dongle's internal write authorization state.
        /// </summary>
        /// <returns></returns>
        public static void AuthorizeWrite()
        {
            KFUNC(WRITEAUTH, WriteCode1, WriteCode2, WriteCode3);
        }

        /// <summary>
        /// Reads the 16-bit serial number from the dongle
        /// </summary>
        /// <returns></returns>
        public static ushort ReadSerialNo()
        {
            uint retval = KFUNC(GetSN, 0, 0, 0);
            return (ushort)retval;
        }

        /// <summary>
        /// Read dongle expiration date
        /// </summary>
        /// <returns></returns>
        public static DateTime ReadExpirationDate()
        {
            //Lease expiration constants
            const int LEASEOK = 65535;
            const int LEASEEXPIRED = 65534;
            const int SYSDATESETBACK = 65533;
            const int NOLEASEDATE = 65532;
            const int LEASEDATEBAD = 65531;
            const int LASTSYSDATECORRUPT = 65530;
            const int CKLEASEDATE = 9;
            const String Title = "LEASE DATE CHECK";
            const int GETEXPDATE = 8;
            String msg;
            int DateRead;
            String DaysLeft;
            int SystemDay;
            int SystemMonth;
            int SystemYear;
            int ExpirationDay;
            int ExpirationMonth;
            int ExpirationYear;
            int BaseYear = 1990;

            KTASK(CKLEASEDATE, 0, 0, 0);
            DateRead = ReturnValue1; //YYYYYYYM MMMDDDDD
            DaysLeft = System.Convert.ToString(ReturnValue2);
            SystemYear = BaseYear + (int)(DateRead / 512);
            SystemMonth = (int)((DateRead & 480) / 32);
            SystemDay = DateRead & 31;
            msg = "The system reports the date as :" + System.Convert.ToString(SystemMonth) + "/" + System.Convert.ToString(SystemDay) + "/" + System.Convert.ToString(SystemYear) + "." + (char)(10) + (char)(13);
            //Acquire and Convert lease expiration date to readable format from storage format
            KTASK(GETEXPDATE, 0, 0, 0);
            DateRead = ReturnValue1; //YYYYYYYM MMMDDDDD
            ExpirationYear = BaseYear + (int)(DateRead / 512);
            ExpirationMonth = (int)((DateRead & 480) / 32);
            ExpirationDay = DateRead & 31;

            return new DateTime(ExpirationYear, ExpirationMonth, ExpirationDay);
        }

        public static bool DoesPositionHasValue(int Position, ushort Value)
        {
            return ReadRegister(Position) == Value;
        }

        /// <summary>
        /// Reads a 16-bit value from one of the 56 registers in the dongle's non-volatile RAM.
        /// </summary>
        /// <returns></returns>
        public static ushort ReadRegister(int reg)
        {
            if (reg < 0 && reg > 55)
                return 0;
            uint retval = KFUNC(GetVarWord, reg, 0, 0);
            return (ushort)retval;
        }

        /// <summary>
        /// Performs a left-shift rotation operation
        /// </summary>
        /// <param name="val"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private static ushort RotateLeft(ushort val, int n)
        {
            int ival = val << n;
            return (ushort)((ival & 0xffff) | (ival >> 16));
        }

        /// <summary>
        /// Perform operations directly on dongle
        /// </summary>
        /// <param name="Arg1"></param>
        /// <param name="Arg2"></param>
        /// <param name="Arg3"></param>
        /// <param name="Arg4"></param>
        public static void KTASK(int Arg1, int Arg2, int Arg3, int Arg4)
        {
            long lgArg1 = Arg1;
            if (lgArg1 < 0)
            {
                lgArg1 = lgArg1 + 2 ^ 16;
            }
            long lgArg2 = Arg2;
            if (lgArg2 < 0)
            {
                lgArg2 = lgArg2 + 2 ^ 16;
            }
            long lgArg3 = Arg3;
            if (lgArg3 < 0)
            {
                lgArg3 = lgArg3 + 2 ^ 16;
            }
            long lgArg4 = Arg4;
            if (lgArg4 < 0)
            {
                lgArg4 = lgArg4 + 2 ^ 16;
            }

            long kfuncRet = KFUNC((int)lgArg1, (int)lgArg2, (int)lgArg3, (int)lgArg4);
            //KTASK = ShowLastKeyError(Err.LastDLLError);
            long returnValue1Long = kfuncRet % 65536;
            ReturnValue1 = (int)returnValue1Long;
            ReturnValue2 = (int)(kfuncRet / 65536);
        }

        #endregion

        #region Dongle Checks

        /// <summary>
        /// I created this method to help me remember to call it before starting because without calling it the dongle logic here will act crazy
        /// and return wrong values from dongle, the reason i didn't call it initially is that i thought it is just a check for dongle presense
        /// but looks like it is more than that!
        /// </summary>
        /// <returns></returns>
        public static bool InitializeDongleActions()
        {
            return IsPresent();
        }

        /// <summary>
        /// Checks for presence of dongle. 
        /// Note that using this function resets read/write authorization
        /// state bits in the dongle, so read/write must be re-authorized after calls to this function.
        /// </summary>
        /// <returns>true if the dongle is present and the authorization sequence passes.</returns>
        public static bool IsPresent()
        {
            int commandArgLong = Portoption * 8192 + KlCheck;

            if (commandArgLong > 32767)
            {
                commandArgLong = commandArgLong - 65536;
            }

            KTASK(commandArgLong, ValidateCode1, ValidateCode2, ValidateCode3);

            // First call DLL with 3 validate codes
            uint retval = KFUNC(commandArgLong, ValidateCode1, ValidateCode2, ValidateCode3);
            var retLow = (ushort)(retval & 0xffff);
            var retHigh = (ushort)(retval >> 16);
            // Next call DLL with first call result processed with canned logic
            retval = KFUNC(
                RotateLeft(retLow, retHigh & 7) ^ ReadCode3 ^ retHigh,
                RotateLeft(retHigh, retLow & 15),
                (ushort)(retLow ^ retHigh),
                0
            );
            retLow = (ushort)(retval & 0xffff);
            retHigh = (ushort)(retval >> 16);
            // If all is well, the returned value will match the client id code.
            if (retLow == ClientIDCode1 && retHigh == ClientIDCode2)
                return true;
            return false;
        }

        /// <summary>
        /// Checks if the dongle is compatibale with the application
        /// </summary>
        /// <returns></returns>
        public bool IsCorrectDongle()
        {
            int commandArgLong = Portoption * 8192 + KlCheck;

            if (commandArgLong > 32767)
            {
                commandArgLong = commandArgLong - 65536;
            }

            KTASK(commandArgLong, ValidateCode1, ValidateCode2, ValidateCode3);

            // First call DLL with 3 validate codes
            uint retval = KFUNC(commandArgLong, ValidateCode1, ValidateCode2, ValidateCode3);
            var retLow = (ushort)(retval & 0xffff);
            var retHigh = (ushort)(retval >> 16);
            // Next call DLL with first call result processed with canned logic
            retval = KFUNC(
                RotateLeft(retLow, retHigh & 7) ^ ReadCode3 ^ retHigh,
                RotateLeft(retHigh, retLow & 15),
                (ushort)(retLow ^ retHigh),
                0
            );
            retLow = (ushort)(retval & 0xffff);
            retHigh = (ushort)(retval >> 16);
            // If all is well, the returned value will match the client id code.
            if (retLow == ClientIDCode1 && retHigh == ClientIDCode2)
                return true;
            return false;
        }

        /// <summary>
        /// Check the expiry date of the keylok dongle
        /// </summary>
        public DongleState CheckDongle()
        {
            DongleState dateState;

            try
            {
                int dateRead;

                //Check for leased dongle and expiration date
                //Lease expiration constants
                KTASK(2, ReadCode1, ReadCode2, ReadCode3);//'2 is the ReadAuth

                //now check that dongle is for Vital
                KTASK(4, StaticKeys.VitalSecurityAddress, 99, 99);//Read
                if (ReturnValue1 != StaticKeys.VitalSecurityNumber)
                {
                    dateState = DongleState.NotCompitable;
                }
                else
                {
                    KTASK(Ckleasedate, 0, 0, 0);
                    switch (ReturnValue2)
                    {
                        case Leaseexpired: //Lease has expired

                            //Acquire and Convert lease expiration date to readable format from storage format
                            KTASK(Getexpdate, 0, 0, 0);

                            dateRead = ReturnValue1; //YYYYYYYM MMMDDDDD
                            _expirationYear = BaseYear + dateRead / 512;
                            _expirationMonth = (dateRead & 480) / 32;
                            _expirationDay = dateRead & 31;

                            var expirationDate = new DateTime(_expirationYear, _expirationMonth, _expirationDay);
                            var nonLeasedDongleDate = new DateTime(2050, 1, 1);

                            dateState = expirationDate == nonLeasedDongleDate ? DongleState.NonLeasedDongle : DongleState.LeaseExpired;

                            break;
                        case Sysdatesetback: //System date is earlier than one set in KEY-LOK
                            dateState = DongleState.SysDateSetBack;
                            break;
                        case Noleasedate: //No lease expiration date has been programmed
                            dateState = DongleState.NoLeaseDate;
                            break;
                        case Leasedatebad: //An invalid lease expiration date exists
                            dateState = DongleState.LeaseDateBad;
                            break;
                        case Lastsysdatecorrupt: //Last system date as stored in device has become corrupt
                            dateState = DongleState.LastSysDateCorrupt;
                            break;
                        default: //Default - Lease has not expired
                            dateState = DongleState.Default;
                            //Lease has not expired
                            //Convert current date returned by security system to readable format
                            int daysLeft = ReturnValue2;
                            KTASK(Getexpdate, 0, 0, 0);
                            dateRead = ReturnValue1; //YYYYYYYM MMMDDDDD
                            _expirationYear = BaseYear + dateRead / 512;
                            _expirationMonth = (dateRead & 480) / 32;
                            _expirationDay = dateRead & 31;

                            if (daysLeft < 5)
                            {
                                dateState = DongleState.LessThanFiveDays;
                            }
                            else if (daysLeft < 10)
                            {
                                dateState = DongleState.LessThanTenDays;
                            }
                            break;
                    }
                }
                
            }
            catch (Exception)
            {
                throw new VitalBaseException("Error occurred during CheckForKEYLOK");
            }
            return dateState;
        }

        #endregion

        #region Dongle High Level Operations
        
        /// <summary>
        /// Gets dash seperated codes as the current codes of the dongle to be used by our staff to update the dongle remotly
        /// </summary>
        /// <returns></returns>
        public string GetDongleRequestCode()
        {
            //Perform Read/Write authorization before perform actions
            AuthorizeRead();
            AuthorizeWrite();
            
            //Get the first two codes
            KTASK(REMOTEUPDUPT1, 0, 0, 0);
            var code2 = ReturnValue1.ToString();
            var code3 = ReturnValue2.ToString();

            //Get the second two codes
            KTASK(REMOTEUPDUPT2, 0, 0, 0);
            var code4 = ReturnValue1.ToString();
            var code5 = ReturnValue2.ToString();

            //Calcuate CheckSum (Code 1) based on the 4 previous codes
            var checksum = (int.Parse(code2) + int.Parse(code3) + int.Parse(code4) + int.Parse(code5)) % 65536;
            var code1 = checksum;

            return code1 + " - " +
                   code2 + " - " +
                   code3 + " - " +
                   code4 + " - " +
                   code5;
        }

        /// <summary>
        /// Reprogram the dongle using codes generated by our staff
        /// </summary>
        public ProcessResult UpdateDongle(int checksum, int code1, int code2, int code3)
        {
            var result = ProcessResult.Failed;

            try
            {
                //Calcualte CheckSum based on codes
                var checkValue = (code1 + code2 + code3) % 65536;

                //Validate correct CheckSum
                if (checkValue == checksum)
                {
                    //Perform update on dongle
                    KTASK(REMOTEUPDUPT3, code1, code2, code3);

                    //Validate the success of the dongle update operation
                    if ((ReturnValue1 != 0 | ReturnValue2 == Remotegetmemory | ReturnValue2 == Remotereplace | ReturnValue2 == RemoteDateExtend))
                    {
                        const int xorValue = ClientIDCode1 ^ ClientIDCode2;
                        var sendValue1 = ReturnValue1 ^ xorValue;
                        var sendValue2 = ReturnValue2 ^ xorValue;
                        checksum = (0 + sendValue1 + sendValue2 + xorValue) % 65536;
                        
                        result.IsSucceed = true;
                    }
                    else
                    {
                        result.IsSucceed = false;
                        result.Message = StaticKeys.DongleResetErrorCouldNotVerifySuccess;
                    }
                }
                else
                {
                    result.IsSucceed = false;
                    result.Message = StaticKeys.DongleResetErrorIncorrectCodes;
                }
            }
            catch (Exception exception)
            {
                result.IsSucceed = false;
                result.Message = exception.Message;
            }

            return result;
        }

        /// <summary>
        /// Set the dongle expiry date
        /// </summary>
        /// <param name="MonthValStr"></param>
        /// <param name="DayValStr"></param>
        /// <param name="YearValStr"></param>
        public static void SetExpDate(String MonthValStr, String DayValStr, String YearValStr)
        {
            long WriteDate;
            int WriteDateInt;
            int DayVal;
            int MonthVal;
            int YearVal;
            int BaseYear;
            BaseYear = 1990;
            MonthVal = 0;
            DayVal = 0;
            YearVal = 0;
            try
            {
                MonthVal = System.Convert.ToInt32(MonthValStr);
                if (MonthVal == 0)
                    throw new System.Exception();

                DayVal = System.Convert.ToInt32(DayValStr);
                if (DayVal == 0)
                    throw new System.Exception();
                YearVal = System.Convert.ToInt32(YearValStr);
                if (YearVal == 0)
                    throw new System.Exception();
                WriteDate = 512 * (YearVal - BaseYear) + 32 * MonthVal + DayVal;
                if (WriteDate > 32767) WriteDate = WriteDate - 2 ^ 16;

                WriteDateInt = (int)WriteDate;
                KFUNC(SETEXPDATE, 0, WriteDateInt, 0);
            }
            catch
            {
                //Code for handling your exception goes here
            }
        }

        #endregion

        #endregion
    }
}
