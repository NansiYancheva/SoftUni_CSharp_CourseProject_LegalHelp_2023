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

            public const int ReviewStarsMin = 1;
            public const int ReviewStarsMax = 10;
        }

        public static class UserConstants
        {
            public const int PasswordMinLength = 6;
            public const int PasswordMaxLength = 100;

            public const int FirstNameMinLength = 1;
            public const int FirstNameMaxLength = 15;

            public const int LastNameMinLength = 1;
            public const int LastNameMaxLength = 15;

            public const int EmailMinLength = 8;
            public const int EmailMaxLength = 40;
        }


    }
}
