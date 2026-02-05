<div id="top"></div>

<!-- PROJECT LOGO -->
<br />

<div align="center">

<h3 align="center">Planning Board</h3>

  <p align="center">
    The Streaming App is used to get data from Twitch and Youtube(future) to show chat and other activities
  </p>
</div>

<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->

## About The Project

Work in Progress

Able to manage and See Chat changes from Twitch and YouTube(future)

Able to Manage Responses, Alerts, Activities and more

<p align="right">(<a href="#top">back to top</a>)</p>

### Built With

#### Backend

- [ASP.NET Core 10](https://learn.microsoft.com/en-us/aspnet/core/overview?view=aspnetcore-10.0)
- [MSSQL](https://docs.microsoft.com/en-us/sql/sql-server/?view=sql-server-ver16)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [NSwag](https://github.com/RicoSuter/NSwag)
- [SignalR](https://learn.microsoft.com/en-us/aspnet/signalr/overview/getting-started/introduction-to-signalr)

- Twtich API [TwitchLib](https://github.com/TwitchLib)
- Bluesky API [idunno.Bluesky](https://bluesky.idunno.dev/?tabs=commandLine%2Csession%2CpostCreate)

#### Fronetnd

- [Angular 21](https://angular.io/)
- [Material](https://material.angular.io/)
- [RxJS](https://rxjs.dev/)
- [Ngrx](https://ngrx.io/)
- [SignalR](https://learn.microsoft.com/en-us/aspnet/signalr/overview/getting-started/introduction-to-signalr)

<p align="right">(<a href="#top">back to top</a>)</p>

<!-- GETTING STARTED -->

## Getting Started

To get started feel free to clone this repository to your local machine.

### Prerequisites

Backend
For .net 10 "Visual Studio 2026" or higher is needed to run the Backend

Frontend
npm version 22.21.1 is used.

## Installation

### Backend

Use Visual Studio 2026

DB will automatically be created / updated but no data will be added.
NSwag will automatically create "api.service.ts" when backend is started.

Manage User secrets and add "ClientId", "ClientSecret", "Channel" and "ChannelId" for Connecting it with Twitch

### Frontend

run "npm install" and start Frontend by "npm run start"

backend Needs to be run with "Visual Studio" no run backend from frontend implemented jet.

## Roadmap

## Contact

Patrick - pyxtrick.business@gmail.com

<p align="right">(<a href="#top">back to top</a>)</p>
