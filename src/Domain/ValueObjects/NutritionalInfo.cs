namespace NutriTrack.src.Domain.ValueObjects
{
    public record NutritionalInfo
    {
        public decimal Calories { get; init; }
        public decimal Protein { get; init; }
        public decimal Carbs { get; init; }
        public decimal Fat { get; init; }

        public NutritionalInfo(decimal calories, decimal protein, decimal carbs, decimal fat)
        {
            if (calories < 0 || protein < 0 || carbs < 0 || fat < 0)
            {
                throw new ArgumentException("Os valores nutricionais não podem ser negativos.");
            }
            Calories = calories;
            Protein = protein;
            Carbs = carbs;
            Fat = fat;
        }
        public static NutritionalInfo Zero => new(0, 0, 0, 0);

        public static NutritionalInfo operator +(NutritionalInfo a, NutritionalInfo b)
        {
            return new NutritionalInfo(
               a.Calories + b.Calories,
               a.Protein + b.Protein,
               a.Carbs + b.Carbs,
               a.Fat + b.Fat
           );
        }
    }
}

