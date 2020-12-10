
[System.Serializable]
public class NoteData
{
    public float ArrivalTime;
    public int LineNumber;

    public NoteData(int LineNumber, float ArrivalTime)
    {
        this.LineNumber = LineNumber;
        this.ArrivalTime = ArrivalTime;
    }

}
