using NutriTrack.src.Domain.ValueObjects;
using System.Diagnostics.CodeAnalysis;

namespace NutriTrack.src.Domain.Entities
{
    public class Food
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }


        public NutritionalInfo BaseNutritionalInfo { get; set; } = NutritionalInfo.Zero;

       
        public Food() { }

        [SetsRequiredMembers]
        public Food(string name, NutritionalInfo baseInfo)
        {
            Id = Guid.NewGuid();
            Name = name;
            BaseNutritionalInfo = baseInfo;
        }

    }
}
