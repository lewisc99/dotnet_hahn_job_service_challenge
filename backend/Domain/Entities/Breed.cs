namespace Domain.Entities
{
    public class Breed
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int LifeMin { get; set; }
        public int LifeMax { get; set; }
        public int MaleWeightMin { get; set; }
        public int MaleWeightMax { get; set; }
        public int FemaleWeightMin { get; set; }
        public int FemaleWeightMax { get; set; }
        public bool Hypoallergenic { get; set; }
    }
}
