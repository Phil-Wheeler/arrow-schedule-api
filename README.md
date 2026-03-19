# Arrow : Scheduling API



## Table of Contents

- [Standard Readme](#standard-readme)
	- [Table of Contents](#table-of-contents)
	- [Background](#background)
    - [Technical Stack](#technical-stack)
	- [Install](#install)
	- [Usage](#usage)
	- [Contributing](#contributing)
		- [Contributors](#contributors)
	- [License](#license)

## Background

The Arrow scheduling API is a basic implementation of a typical scheduling system's core requirements common across most customer booking systems. The project started as a design concept for an existing business domain problem where legacy code and technical debt were forcing conversations about the need for a standardised, well-architected API. Intially intended to serve as a learning exercise and discussion document, the services developed into a foundation for any basic scheduling system's skeleton.

The endpoints and models provided include:

* Business details
* Booking information
* Service and location details
* Customer information
* Role based access control via JWT


## Technical Stack

The solution is written as a .Net Web API project using version 10 of the C# runtime, backed by a PostgreSQL database.

## Install

To run this solution, the Microsoft .Net 10 SDK should be installed first. A PostgreSQL database should also be available. The solution assumes the use of .Net user secrets for local development, but appsettings or environment variables would work just as well.

Run the project using `dotnet run` from the root folder (i.e. the same folder as Arrow.csproj).


```sh
$ cd Arrow
$ dotnet run
```

## Usage

This is a relatively simple and naive implementation of business logic and as such, can be used for any purpose - commercial, personal or charitable - without attribution. Please ensure you check the [licence terms]("licence.md") below before using this code.

## Contributing

Feel free to contribute improvements! [Open an issue](https://github.com/Phil-Wheeler/arrow-schedule-api/issues/new) or submit PRs.

Arrow Scheduling API follows the [Contributor Covenant](http://contributor-covenant.org/version/1/3/0/) Code of Conduct.

### Contributors

This project exists thanks to all the people who contribute. 
<a href="https://github.com/Phil-Wheeler/arrow-schedule-api/graphs/contributors"><img src="https://opencollective.com/standard-readme/contributors.svg?width=890&button=false" /></a>


## License

[COOPERATIVE NON-VIOLENT PUBLIC LICENSE](https://github.com/Phil-Wheeler/arrow-schedule-api/blob/main/licence.md) © Phil Wheeler