using SqlSugar;

namespace UnitOfWork.Domain
{ 
    public class Student
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)] 
        public long Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}