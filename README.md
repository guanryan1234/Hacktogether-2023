# AI Assist:
This project was completed Yusuf Corr and Ryan Guan for the Hacktogether 2023
 
AI Assist is an open AI based project that integrates various Microsoft Graph endpoints and AI fine fine-tuned models to automate the scheduling of tasks in the Microsoft To-Do app that have to do with creating and scheduling a meeting. In short, AI Assist will auto-complete those specific tasks and add a specific teams meeting to your calendar, along with automating the invite to any user in your particular org.  

# Model Training:
Open AI “davinci” was the base model used in fine-tuning the AI Assist model. Essentially, multiple prompts such as the following were passed in with different completions resulting in the model being trained to be able to interpret when a user passed in a schedule based prompt. From that prompt, the AI model was able to extrapolate who, what, when from the prompt and return it as a JSON format. 
Example Prompt: 
"prompt": "Schedule a meeting with John Smith, discussing the new product launch, on Monday at 10 am.", 
Example Completion:
"completion": "{\"who\": \"John Smith\", \"what\": \"discussing the new product launch\", \"when\": \"13-03-2023\"}"} 
After training with more than 200 data points, the open AI model was able to succeed in extrapolate and interpret data with high accuracy. This allows us to directly use this model when searching for and obtaining data from a prompt. 

# Microsoft Graph Endpoints:
The various Microsoft Graph Endpoints were used in making this application:
•	me/todo: Accessing to-do task list, tasks, and completing said task
•	me: Displaying user data
•	me/events: Creating a meeting using meeting details (obtained from the AI Assist Model reading a To Do task)
•	search/query: We used the search/query endpoints to search for users in our particular organization (when applicable)
The graph endpoints were paramount in the automation flow of reading to-do tasks, scheduling a Teams meeting, adding an attendee, and completing the to-do task. After the to-do task was determined to be a schedule based task. We then fed the AI assist model the task title. Based on what the AI model was able to return, we could then use meeting details and directly create a Teams meeting for the user. 

# Roadmap:
Given the constraints of the Hackathon, we were not able to complete the following. Given more time, here is where we would have planned for the future.
•	Find meeting time graph endpoint: Using another endpoint from Graph, we could have found a meeting time that fit both users before scheduling
•	Time guard rails: We did not have the time nor understand how this particular scenario would work, but we would have wanted to create guard rails around our systems scheduling process where we could only schedule meetings between both attendees work hours
•	Adding additional attendees: Currently, our system can only add one attendee to our organization. This would have been improved upon to add multiple attendees inside our organization to the meeting. 
•	Flexible search of attendees: Currently, our system can only search for attendees by first and last name. If there are multiple attendees with the same first and last name, it would pick the first search result returned. Of course, this is proof of concept, given additional time we would have made the search more robust to search by additional distinct parameters.

# How To Run: 
NOTE: You must locate and paste the bearer token from your authenticated Microsoft Graph directly into the app settings JSON. 
To run, download and launch this product from Visual Studios 2022. 
Once the application runs, open up your Microsoft To-Do (make sure that both your Graph, Microsoft To-Do and Teams are authenticated under the same account and have at least one person in that particular organization.
Add a couple tasks into your Microsoft To-Do. For tasks you would like to have automatically scheduled, use this format.
	Schedule <MEETING DESCRIPTION> with <PERSON> at <TIME> 
For time, make sure it’s in a similar format as DD-MM-YYYY  or day of week. Day would be an integer value and month would be a string value of the month. 
•	Schedule a meeting with John Smith next Tuesday at 3:00 PM
•	Schedule a meeting with Sam Adams discussing board test results for May 5th 
You can add other to-do tasks that aren’t as specific for additional test data. After adding your set number of tasks from the AI assist application, click the button titled “Run AI Assist”! Wait 1-2 minutes and you will soon see data populated in a table format. 
The data presented in the AI application represents tasks that were handled and completed by the AI assist service. This means in navigating Microsoft To-Do, you should the same tasks completed and if you navigate to Teams, you should see a meeting scheduled with a particular title specifically representing that task.
 NOTE: The scheduling constraints only schedule meetings within datetime.now. A future add would be to train the models to schedule based on the exact dates given. 
