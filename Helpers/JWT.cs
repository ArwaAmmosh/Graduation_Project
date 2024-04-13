namespace Graduation_Project.Helpers
{
    public class JWT
    {
#pragma warning disable CS8618 // Non-nullable property 'Key' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string Key { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Key' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'Issuer' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string Issuer { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Issuer' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'Audience' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string Audience { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Audience' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'DurationInDays' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string DurationInDays { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'DurationInDays' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.

    }
}
