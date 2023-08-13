using System.ComponentModel.DataAnnotations;

namespace Todo_List_App
{
    public class TodoItem
    {
        [Key] 
        public virtual int Id { get; set; }

        public virtual string Task { get; set; }

        public virtual bool IsCompleted { get; set; }

        public virtual int Priority { get; set; }
    }
}
