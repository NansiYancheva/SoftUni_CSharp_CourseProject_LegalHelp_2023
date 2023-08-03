LegalHelp


A web application for receiving legal help ⚖️ - legal advice and template of documents 📜 through requests (tickets)

🎯 My project for the ASP.NET course at SoftUni. (June - August 2023)



ℹ️ How It Works

Users visitors:
- can register
- can login
- can see the home page
- can see all tickets (which includes both ticket for document and ticket for legal advice) but in order to see the result and the reviews of the ticket they should register or login

Logged Users:
- can see the home page
- can add new ticket 
- can see all tickets (which includes both ticket for document and ticket for legal advice), their result and their reviews and the name and the reviews of the team member who resolve the ticket
- can sort the tickets by resolved/unresolved status
- can also review the result and the team member
- can see only their tickets and can delete just those tickets
- can edit their tickets only if they are not resolved
- can select only to see all documents or the documents they have downloaded
- can select only to see all legal advices or the legal advices they have received
- can see all team members and add or view their reviews
- can edit their data
- can delete their account

Legal Advisor:
Can be added by the admin. Meaning an user should be register first and after that the admin can make them a legal advisor.
- can do/see everything what the user do plus can add legal advices to tickets with category request for legal advice
- can see all the legal advices which they gave

Uploader:
Can be added by the admin. Meaning an user should be register first and after that the admin can make them an uploader.
- can do/see everything what the user do plus can add documents to tickets with category request for document
- can see all the documents which they uploaded

💭 The reason for having a legal advisor and an uploader is that in order to give legal advices the person need to have enought experience but it is not neccessary to have such to upload a template of a document if you know what kind of the document does the user need and you know where to find the document

Admin:
- can do/see everything what the user, legal advisor, uploader do 
- have admin home page which redirects to other pages
- can make user uploader/legal advisor

TO be updated:
- to edit a ticket with resolved status
- to delete only legal advice to the ticket and not the ticket itself
- to edit(change)/delete document to the ticket and not the ticket itself
- to manage all reviews for tickets, legal advices, documents, team members - meaning to delete/edit/add
- to create/edit/delete user - to be checked if this will be in compliance with GDPR - or only to be done for legal advisor/uploader
- to remove the "role" of legal advisor/uploader from an user


⚒️ Built With

ASP.NET 6.0
Microsoft SQL Server
ASP.NET Identity 
MVC Areas
Partial Views
View Components
Data Validation, both Client-side and Server-side
Data Validation in the Models and Input View Models


⚙️ Application Configurations

1. Connection string
    In appsettings.json you should include your connection string and its name should be "DefaultConnection"

2. Database Migrations
    would be applied when you run the application, since the ENVIRONMENT is set to Development. If you change it, you should apply the migrations yourself.

3. Configuring sample data
    Once you run the application, you should create Test Accounts.
    The admin must be admin@legalhelp.bg because it is set as DevelopmentAdminEmail.
    After that you should create two users - one for legal advisor and one for uploader and through the admin profile to make them in these roles.
    At the end you should test the application with a user.

TO be updated:
To be seeded admin, legal advisor, uploader and one user


🔝 Acknowledgments

Using ASP.NET-MVC-Template developed by:
Kristiyan Ivanov
And features by:
Stamo Petkov


🔎 Resources

https://blog.ipleaders.in/legal-world-trends/ - home page picture
