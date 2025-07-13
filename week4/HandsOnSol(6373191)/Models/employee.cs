namespace WebApplication1.Models
{
    public class employee
    {
       
            public int Id { get; set; }
            public string Name { get; set; }
            public int Salary { get; set; }
            public bool Permanent { get; set; }
            public Department Department { get; set; }
            public List<Skill> Skills { get; set; }
            public DateTime DateOfBirth { get; set; }
        

    }
}
