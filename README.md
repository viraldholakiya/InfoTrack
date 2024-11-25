# InfoTrack

Instructions:

1. The api is written using .net 8 framework.
2. I have tried to share my experience working in clear architecture applications.
3. "1-App" folder contains all applications. But for now, it's just InfoTrack.App.Api project. Which should be the start up project.
4. This api application has only one post end-point to book meetings. Here is the detail of it

TYPE: POST
URL:https://localhost:7207/Bookings
BODY:	{
			"bookingTime": "09:30",
			"name":"John Smith"
		}

5. By running the application, you will be able see a nice swagger page at https://localhost:7207/swagger/index.html
6. For given time limit to develop the code, I haven't considered Authentication, Caching or Logging. We can discuss the topics in the interview if they were required.
7. I have injected a class haveing list of booking as Singleton to use it as kind of In-memory database.
        services.AddSingleton(typeof(BookingContext));


Overall, I tried to keep things simple but at the same time showed some of my technical skills of my experience.
