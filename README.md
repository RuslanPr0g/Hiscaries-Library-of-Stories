# 📚 Hiscaries Library of Stories

Welcome to the **Hiscary Library of Stories** repository! This project (referred to as _application_) is undergoing a complete refactor, following the best practices known to date. 🛠️

## 🚀 Refactoring Plan for the Server 🚀

The following steps outline the server refactoring process:

0. **Performance**

   - Take a look at different places and see where we need to improve performance
   - For example, recommendations endpoint gives us all the stories, without any limitations

1. **Improve Identity Provider**

   - Move identity providing (jwt, oauth, etc.) to a separate service (OpenIddict as a separate AUTH server with its separate DB, etc.).

2. **Domain Layer**

   - Refactor domain entities to include domain-specific methods.
   - Add domain services

3. **Deployment**

   - Streamline the deployment process to allow for one-click application deployment.

4. **Auth and Security**

   - Add roles to the system and also validation that user is authorized to perform certain actions.

5. **DTO Optimization** 🚀

   - Simplify Read DTOs, ensuring they only carry necessary data.

6. **Application Layer** 🚀

   - Split the application layer into _Read_ and _Write_:
   - **Read**: Utilize Dapper with Read Models (DB Views).
   - **Write**: Use EF Core with Change Tracking and strict transactional boundaries within one aggregate root.

7. **Pagination Implementation**

   - Implement pagination when fetching data

8. **REST API Best Practices** 🚀

   - Refactor API to adhere to the latest REST best practices.
   - Implement security roles to restrict access to authorized users for specific actions.

9. **Persistence Layer**

   - Refactor persistence logic to use EF Core for writing and Dapper for reading.

10. **SQL Views & Migrations**

    - Add view SQL generation as part of EF Core migrations.

11. **Global Exception Handling**

    - Implement global exception handling.

12. **Logging**

    - Add comprehensive system logging.

13. **Authorization & Role Checks**

    - Ensure proper authorization so that users can only delete their own content, while admins have broader rights (e.g., delete all stories or comments).

14. **Docker**

    - Add Docker to encapsulate the entire system for easy deployment.

15. **Optimistic Concurrency**

    - Implement optimistic concurrency control.

16. **Audit Fields**

    - Add audit fields (e.g., `CreatedAt`, `UpdatedAt`) for all entities.

17. **Pagination**

    - Add pagination for large datasets (e.g., search results).

18. **Caching**

    - Implement caching at all levels: frontend, HTTP, API, Redis, DB, etc.

19. **TODO Fixes**

    - Address all existing TODOs.

20. **Dev Testing**

    - Perform thorough testing to ensure the system is robust, especially in scenarios involving high interaction (e.g., adding content while editing stories or performing repeated actions).

21. **Unit & Integration Tests**

    - Add unit tests and integration tests for full coverage.

22. **Specification Pattern**

    - Introduce the specification pattern to repositories.

23. **Domain Events**

    - Implement domain events.

24. **Other microservices**
    - Think about what other microservice to add that would leverage event sourcing. But it should be reasonable in terms of my app.

## 🌟 Future Improvements & Features

1. **AI/ML Integration**

   - Create a separate microservice that uses AI/ML to generate stories.
   - Simulate user activity with AI to enhance logging, metrics, and system performance insights.

2. **User-Uploaded PDFs**

   - Allow users to upload their own PDF files (pending admin approval due to copyright).

3. **Private Stories**
   - Add an option for private stories that can only be shared via one-time links or similar mechanisms.

---

## 🎨 Refactoring Plan for the Client

1. **Component Separation**

   - Split the frontend into reusable components.

2. **State Management**

   - Integrate NgRx for state management. Learn more [here](https://ngrx.io/guide/store).

3. **Mobile Support**
   - Ensure the app is mobile-responsive.

---

## 🛠️ Configuration Settings

Configure your settings in your chosen config file as follows:

```json
{
  "Kestrel:Certificates:Development:Password": "***",
  "JwtSettings:Key": "***",
  "JwtSettings:Issuer": "***",
  "JwtSettings:Audience": "***",
  "SaltSettings:StoredSalt": "***",
  "ConnectionStrings:PostgresEF": "Server=postgresdb;Port=5432;User Id=postgres;Password=***;Database=hiscarydbef;Include Error Detail=true;"
}
```

In your `.env` file (to be located next to `docker-compose.yml`), include the following:

```
POSTGRES_PASSWORD=***
```

---

🎉 **Thank you for visiting the repository!**  
Feel free to contribute or raise any issues to improve the application!

---

Some more feature ideas from CHAT-GPT:

### Small Features

1. **User Profiles**  
   Enable users to create and update their profiles, including bio, avatar, and links to social media. This will allow you to build user-specific data models and practice data binding in Angular.

2. **Story Ratings & Reviews**  
   Let users rate and review stories. Display average ratings and highlight top-rated stories. This will involve building a ratings service and handling real-time updates to reflect ratings in the UI.

3. **Bookmarks or Favorites**  
   Allow users to save stories to a "Favorites" list. This will require adding a collection in the user profile and managing many-to-many relationships in the backend.

4. **Story Search and Filter**  
   Implement a search feature to filter stories by title, author, genre, etc. Experiment with indexing in the database for faster retrieval and integrate Angular form controls for UI.

5. **Reading Progress**  
   Track the last page read for each user for every story and display it as "Resume reading." This will involve capturing progress and implementing an API to save and retrieve this data efficiently.

6. **Subscriptions to Authors**  
   Let users follow authors to get notified when new stories are published. This will involve creating a subscription system and using background tasks to manage notifications.

### Medium Complexity Features

7. **Email Notifications**  
   Use background jobs to send notifications for new followers, new story releases, or when a favorited author publishes something. Try Hangfire or Azure Functions for the backend job processing.

8. **Chapter-Based Story Updates**  
   For long stories, allow authors to publish chapters as they complete them. This could involve handling partial saves and versioning in your data model and frontend.

9. **Content Recommendations**  
   Implement a recommendation engine that suggests similar stories based on reading history or genre preferences. Consider building a service for recommendations and experimenting with caching for faster performance.

10. **Story Collaboration**  
    Let users co-author stories, enabling them to invite others to contribute. This will involve permissions and role management, which is great for practicing secure API design.

11. **Real-Time Notifications**  
    Use SignalR or WebSockets to implement real-time notifications for story updates or user messages. This will add interactive features and introduce you to real-time communication in web apps.

12. **Multilingual Support**  
    Enable users to publish stories in different languages, with options for translation. This will involve managing language content in both frontend and backend and allowing users to switch between languages.

### Larger and More Complex Features

13. **Subscription Plans and Premium Content**  
    Create a subscription model where users can access additional features like ad-free reading, offline access, or exclusive stories. This will involve payment integration (e.g., Stripe) and role-based access control.

14. **Story Analytics Dashboard**  
    For authors, create an analytics dashboard showing views, completion rates, and reader demographics. This could include aggregating data using CQRS and building data visualizations in Angular.

15. **Full-Text Search with Indexing**  
    Implement full-text search across story content using Elasticsearch or Azure Cognitive Search. This will make the search feature much faster and more comprehensive.

16. **Recommendation System with Machine Learning**  
    Develop a recommendation system that uses machine learning models to predict stories users may like based on their reading habits. This will involve integrating ML models, likely with ONNX in .NET or Azure ML services.

17. **Interactive Story Elements**  
    Allow authors to embed multimedia, like images, videos, or audio clips, within stories. This will involve handling different content types and their rendering, caching, and streaming.

18. **Offline Mode with PWA Support**  
    Enable offline access to downloaded stories through Progressive Web App (PWA) features. This will involve storing data locally and synchronizing it when the user is back online.

19. **User-Driven Challenges or Story Events**  
    Introduce a feature where authors can create writing challenges, and users can submit stories. This will involve creating challenge entities and implementing criteria-based evaluation.

20. **Gamification (Badges, Achievements, Leaderboards)**  
    Create a gamification system that awards users for actions like publishing, reading milestones, or popular reviews. This will involve creating a rewards service and managing user progress tracking and notification.
