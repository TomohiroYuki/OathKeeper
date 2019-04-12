namespace OathKeeper
{
    public enum ENoteType
    {
        SIMPLE,
        EXTRA
    }
    class NoteBase
    {
        public float time { get; set; } = 0;
        public int line { get; set; } = 0;

        public ENoteType note_type = ENoteType.SIMPLE;
        public NoteBase(float in_time,int in_line,ENoteType in_type)
        {
            time = in_time;
            line = in_line;
            note_type = in_type;
        }
    }

    class SimpleNote : NoteBase
    {
        public SimpleNote(float in_time, int in_line):
            base(in_time,in_line,ENoteType.SIMPLE)
        {


        }
    }

    class ExtraNote : NoteBase
    {
        public ExtraNote(float in_time, int in_line) :
            base(in_time, in_line, ENoteType.EXTRA)
        {


        }
    }

}
