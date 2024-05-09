# Embedded System w/ Web Socket Server

Cortex-M architecture microcontroller will be embedded and run bare metal with rust. The web socket server will be coded in C# and hosted on AWS for low latency and almost 0 downtime.

The embedded rust will interact with this microcontroller and all binary stored in each pin will be sent via web socket connection to the C# server. Endpoints such as /live will allow any end user to view the real time binary by creating a web socket connection of their own, similar to how twitch messages work. /history/pin1, /history/pin1, etc will store the a file created at 24 hours intervals of all the binary created by that pin for the day. Users will have an account with authentication and registration to view this information. Accessing binary data through our bare metal rust in real time will allow us to acess an otherwise offline external microcontroller with a clean front end.


# Tech Stack
* Rust
* C#
* Cotex-M
* AWS server hosting
* Bare metal (Embedded) cargo
* Rest API/Web Socket
* Relational Databse
* User auth
* Visual Studio
* Docker

