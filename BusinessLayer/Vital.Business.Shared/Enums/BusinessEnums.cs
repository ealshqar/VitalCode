using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vital.Business.Shared.Enums
{
    /// <summary>
    /// Loading type enum, this enum will be use in the database repositories and filters to represent the loading type.
    /// </summary>
    public enum LoadingTypeEnum
    {
        All,
        Light,
        None
    }

    /// <summary>
    /// Enum for auto CSA reading mode.
    /// </summary>
    public enum AutoCSAReadingMode
    {
        Continuous,
        StableReading,
        Mixed
    }

    /// <summary>
    /// Enum for hardware types.
    /// </summary>
    public enum HardwareType
    {
        CSA,
        Prototype
    }

    /// <summary>
    /// Enum for types of prototype command.
    /// </summary>
    public enum PrototypeCommand
    {
        Reset,
        SetScanningPoint,
        MoistureCheck,
        PressureCheck
    }

    /// <summary>
    /// Enum for types of prototype responses.
    /// </summary>
    public enum PrototypeResponse
    {
        ValidMoisture,
        InvalidMoisture,
        ValidPressure,
        InvalidPressure
    }

    /// <summary>
    /// Enum for types of AutoCSA command.
    /// </summary>
    public enum AutoCSACommand
    {
        Reset,
        ActivateManualMode,
        ActivateTopPlate,
        ActivateImprinting,
        ActivateAutomationMode,
        StartAutomation,
        StopAutomation,
        SetAutomationProbe,
        HingeCheck,
        MoistureCheck,
        PressureCheck
    }

    /// <summary>
    /// Enum for types of AutoCSA responses.
    /// </summary>
    public enum AutoCSAResponse
    {
        IdleMode,
        IdleAutomationMode,
        ImprintingModeActivated,
        ValidHinge,
        InvalidHinge,
        ValidMoisture,
        InvalidMoisture,
        ValidPressure,
        InvalidPressure,
        ManualProbesDisconnected,
    }

    /// <summary>
    /// Enum for AutoCSAMode.
    /// </summary>
    public enum AutoCSAMode
    {
        Disconnected,
        Idle,
        Manual,
        Automation
    }

}
