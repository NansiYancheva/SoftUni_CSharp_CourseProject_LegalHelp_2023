namespace LegalHelpSystem.Common
{
    public static class EntitiesValidationConstants
    {
        public static class DocumentConstants
        {
            public const int DocumentNameMinLength = 2;
            public const int DocumentNameMaxLength = 30;

            public const int DocumentDescriptionMaxLength = 60;
        }

        public static class DocumentTypeConstants
        {
            public const int DocumentTypeNameMinLength = 2;
            public const int DocumentTypeNameMaxLength = 40;
        }

        public static class UploaderConstants
        {
            public const int UploaderAuthorRightsDescriptionMinLength = 5;
            public const int UploaderAuthorRightsDescriptionMaxLength = int.MaxValue;
        }

        public static class LegalAdvisorConstants
        {
            public const int LegalAdvisorNameMinLength = 5;
            public const int LegalAdvisorNameMaxLength = 30;

            public const int LegalAdvisorEmailMinLength = 8;
            public const int LegalAdvisorEmailMaxLength = 40;

            public const int LegalAdvisorPhoneNumberMinLength = 10;
            public const int LegalAdvisorPhoneNumberMaxLength = 13;
            public const string LegalAdvisorPhoneNumberRegulation = @"^((\+359)|^0{1})(\-|\s)*[0-9]{3}(\-|\s)*[0-9]{2}(\-|\s)*[0-9]{2}(\-|\s)*[0-9]{2}$";

            public const int LegalAdvisorAddressMinLength = 5;
            public const int LegalAdvisorAddressMaxLength = 30;

            public const string LegalAdvisorRatingMinLength = "0";
            public const string LegalAdvisorRatingMaxLength = "10";
        }

        public static class LegalAdviseConstants
        {
            public const int AdviseResponseMinLength = 10;
            public const int AdviseResponseMaxLength = int.MaxValue;
        }

        public static class TicketConstants
        {
            public const int TicketSubjectMinLength = 2;
            public const int TicketSubjectMaxLength = 30;

            public const int TicketDescriptionMinLength = 10;
            public const int TicketDescriptionMaxLength = 500;
        }

        public static class TicketCategoryConstants
        {
            public const int TicketCategoryNameMinLength = 2;
            public const int TicketCategoryNameMaxLength = 50;
        }

        public static class ReviewConstants
        {
            public const int TextReviewMinLength = 8;
            public const int TextReviewMaxLength = 50;

            public const int ReviewStarsMinLength = 0;
            public const int ReviewStarsMaxLength = 10;
        }


    }
}
