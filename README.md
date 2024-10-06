A proof of concept I made for a contact I had in DHL Logistic administration office in Sweden. The goal was to get live updates about all routes the were currently being driven by truck drivers.

How this PoC works:
1. Use Sweden's Trafikverkets API to subscribe to traffic/road events on given routes/locations
2. When an event occurs then use Google Maps Static API to construct an image of the location of the event
3. Use own SMTP server to email the image along with the information from Trafikverket which included what happened, severity, etc

End result
![image](https://github.com/user-attachments/assets/ddc9bf84-e0a6-4154-8343-df51ecddc925)

The end result was promising and showed potential for future applications. This can be combined with Google's other API services to collect even more information about routes.
