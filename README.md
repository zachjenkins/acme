# Acme Outreach Service

### Tools Used

- C# with dotnet core 2.2
- Azure Functions 2.0
    - Timed Triggers
    - Service Bus Triggers
    - Http Triggers
- Azure Cosmos DB (MongoAPI)
- Azure Service Bus Queue
- Azure App Insights

### Functions

WeatherDataCollectionTrigger
- Timer triggered function
- Executes on timed interval
- Retrieves all zip codes registered for collection, and publishes a "weatherUpdateRequestedEvent" for each zipCode.
- Each event is packaged in a single ServiceBus message

WeatherCollectionRequestedHandler
- ServiceBus triggered function
- Subscribes to all "weatherUpdateRequestedEvent" messages
- For each received message, collects weather data for ZipCode via the OpenWeatherMap API and saves it to Cosmos DB collection

GetRegisteredZips
- Http triggered function
- Displays all ZipCodes that are registered for data collection in Cosmos DB collection

GetOutreachRecommendationsFunc
- Http triggered function
- Serves the weather and recommendation data that is stored in Cosmos DB
