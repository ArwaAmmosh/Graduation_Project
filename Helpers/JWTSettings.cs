namespace Graduation_Project.Helpers
{
    public class JWTSettings
    {
        public string Sercet { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public bool ValidationIssuerSigningKey { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidationLifeTime { get; set; }
        public bool ValidateAudience { get; set; }
         public string DurationInDays { get; set; }
   
    }
}
