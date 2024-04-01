namespace Domain.Entities
{
    public class Task : BaseModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }
       
        public string Description { get; set; }
        
        public Common.Enums.TaskStatus Status { get; set; }

        public DateTime CompletionDate { get; set; }

        public Guid CreatedBy { get; set; }
        
        public Guid UserId { get; set; }
    }
}
