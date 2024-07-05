namespace Logic.Models
{
    public class ClassDto
    {
        public string ClassCode { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<StudentDto> Students { get; set; }
    }
}
