namespace LegalHelpSystem.Common
{
    public static class EntitiesValidationConstants
    {
        public static class DocumentConstants
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 30;

            public const int DescriptionMaxLength = 60;
        }

        public static class DocumentTypeConstants
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 40;
        }

        public static class UploaderConstants
        {
            public const int AuthorRightsDescriptionMinLength = 5;
            public const int AuthorRightsDescriptionMaxLength = int.MaxValue;
        }

        public static class LegalAdvisorConstants
        {
            public const int NameMinLength = 5;
            public const int NameMaxLength = 30;

            public const int EmailMinLength = 8;
            public const int EmailMaxLength = 40;

            public const int PhoneNumberMinLength = 10;
            public const int PhoneNumberMaxLength = 13;
            public const string PhoneNumberRegulation = @"^((\+359)|^0{1})(\-|\s)*[0-9]{3}(\-|\s)*[0-9]{2}(\-|\s)*[0-9]{2}(\-|\s)*[0-9]{2}$";

            public const int AddressMinLength = 5;
            public const int AddressMaxLength = 30;

            public const string RatingMinLength = "0";
            public const string RatingMaxLength = "10";
        }

        public static class LegalAdviseConstants
        {
            public const int AdviseResponseMinLength = 10;
            public const int AdviseResponseMaxLength = int.MaxValue;
        }

        public static class TicketConstants
        {
            public const int SubjectMinLength = 2;
            public const int SubjectMaxLength = 20;

            public const int RequestDescriptionMinLength = 10;
            public const int RequestDescriptionMaxLength = 500;
        }

        public static class TicketStatusConstants
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
        }

        public static class TicketCategoryConstants
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
        }


    }
}
