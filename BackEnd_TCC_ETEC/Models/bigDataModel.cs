namespace BackEndAplication.Models
{
    public class bigDataModel
    {
        //public string user { get; set; }
        public string CompleteName { get; set; }
        public string BornDate { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public string Adress { get; set; }
        public string Number { get; set; }
        public string Neighborhood { get; set; }
        public string TeachingInstitution { get; set; }
        public string HaveBF { get; set; }
        public string HaveCadUniq { get; set; }
        public string CityTeachingInstitution { get; set; }
        public string Period { get; set; }
        public string TermsOfUse { get; set; }
        public string MonthStudy { get; set; }
    }

    public class BigDataModel
    {
        public string user { get; set; } = string.Empty;
        public string CompleteName { get; set; } = string.Empty;
        public string OperationDate { get; set; } = string.Empty;
        public string BornDate { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string RG { get; set; } = string.Empty;
        public string Adress { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public string Neighborhood { get; set; } = string.Empty;
        public string TeachingInstitution { get; set; } = string.Empty;
        public string HaveBF { get; set; } = string.Empty;
        public string HaveCadUniq { get; set; } = string.Empty;
        public string CityTeachingInstitution { get; set; } = string.Empty;
        public string Period { get; set; } = string.Empty;
        public string TermsOfUse { get; set; } = string.Empty;
        public string MonthStudy { get; set; } = string.Empty;
    }

    public class BigDataModelUpdate
    {
        public string Adress { get; set; }
        public string Number { get; set; }
        public string Neighborhood { get; set; }
        public string TeachingInstitution { get; set; }
        public string HaveBF { get; set; }
        public string HaveCadUniq { get; set; }
        public string CityTeachingInstitution { get; set; }
        public string Period { get; set; }
        public string MonthStudy { get; set; }
    }
}
