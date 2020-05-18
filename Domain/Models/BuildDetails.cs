namespace MakiseSharp.Domain.Models
{
    public class BuildDetails
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Result { get; set; }
        public string QueueTime { get; set; }
        public string StartTime { get; set; }
        public string FinishTime { get; set; }
        public string Url { get; set; }
        public TriggerDetails TriggerDetails { get; set; }
        public AuthorDetails AuthorDetails { get; set; }
        public PipelineDetails PipelineDetails { get; set; }
        public RepositoryDetails RepositoryDetails { get; set; }
        public ProjectDetails ProjectDetails { get; set; }
    }
}
