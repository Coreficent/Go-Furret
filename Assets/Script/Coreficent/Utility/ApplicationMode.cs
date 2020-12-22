namespace Coreficent.Utility
{
    public class ApplicationMode
    {
        public static Mode State = Mode.Debug;
        public enum Mode
        {
            Debug,
            Release
        }
    }
}
