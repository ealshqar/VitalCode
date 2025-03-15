namespace Vital.Business.Shared.DomainObjects.Hardware
{
    /// <summary>
    /// Represents the string codes of the testing points.
    /// </summary>
    //ToDo: Not used - Remove this class
    public class AutoTestingPoint
    {
        private AutoTestingPoint(string key, string name, SetPointCommands commands)
        {
            Key = key;
            Name = name;
            Commands = commands;
        }

        private static readonly string[] _strArray = { "Lymph", "Nerve", "Circulation", "Master Organ", "Endocrine" };
        private static readonly AutoTestingPoint[] _array = { Lymph, Nerve, Circulation, MasterOrgan, Endocrine };

        public SetPointCommands Commands { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }

        public static AutoTestingPoint Lymph { get { return new AutoTestingPoint("LymphKey", "Lymph", new SetPointCommands("1","6","7")); } }
        public static AutoTestingPoint Nerve { get { return new AutoTestingPoint("NerveKey", "Nerve", new SetPointCommands("2", "8", "9")); } }
        public static AutoTestingPoint Circulation { get { return new AutoTestingPoint("CirculationKey", "Circulation", new SetPointCommands("3", "A", "B")); } }
        public static AutoTestingPoint MasterOrgan { get { return new AutoTestingPoint("MasterOrganKey", "Master Organ", new SetPointCommands("4", "C", "D")); } }
        public static AutoTestingPoint Endocrine { get { return new AutoTestingPoint("EndocrineKey", "Endocrine", new SetPointCommands("5", "E", "F")); } }

        public static string[] ToStringArray()
        {
            return _strArray;
        }

        public static AutoTestingPoint[] ToArray()
        {
            return _array;
        }

        public struct SetPointCommands
        {

            public SetPointCommands(string flashing, string blue, string green)
            {
                Flashing = flashing;
                Blue = blue;
                Green = green;
            }

            /// <summary>
            /// Gets or sets the flashing command.
            /// </summary>
            public string Flashing;

            /// <summary>
            /// Gets or sets the blue command.
            /// </summary>
            public string Blue;

            /// <summary>
            /// Gets or sets the green command.
            /// </summary>
            public string Green;
        }

    }

}