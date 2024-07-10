# Students Code Challenge API

### Purpose

This project is done with the intention of testing my skills with a project that creates a basic RESTFULL API following the structure and clean code practices most commonly used in the industry.

### Content 

A 4 layer Application:

- Data Layer: Database and CRUD functionality.
- Logic Layer: Business logic functionality.
- Presentation Layer: Endpoints and only interaction point with the outside world.
- Test Layer: A Unit test project that tests the managers.

## Getting Started

Clone the repository 

```bash
git clone https://github.com/Vicno/students-code-challenge-api.git
```

Enter the file and run **students-code-challenge-api.sln**

There are 2 valid projects to run inside the file, the **Presentation** layer runs the actual proyect.
The **Tests** layer handles the unit tests. 

### Using the APP

The application runs on https://localhost:7175/ with a swagger interace in https://localhost:7175/swagger/index.html.

There are 2 sets of endpoints, one for students, and one for classes.

Students have a name, lastname and id, the only limitation on the creation and edition of students is their id as the name and lastname could be repeated on a real world usage scenario.

```json
{
    "id": "f2fdf7bb-8b5b-4e42-8c50-dc4bb3cbde2e",
    "name": "John",
    "lastName": "Doe"
  }
```

The Class has a title, description, classCode and a students array of students taking the class.

```json
{
    "classCode": "8b5b-4e42-8c50-dc4bb3cbde2e",
    "title": "Math 101",
    "description": "Basic Math Class",
    "students": [
      {
        "id": "f2fdf7bb-8b5b-4e42-8c50-dc4bb3cbde2e",
        "name": "John",
        "lastName": "Doe"
      }
    ]
  }
```

It's worth noting that the students array internally handles a Guid of the students and not the student objects, the response generated is a consecuence of a conversion to more easily understand and handle the data.

Both endpoints have a valid object definition to create and update entries, for students this is the base structure expected as an input:

```json
{
  "id": "string",
  "name": "string",
  "lastName": "string"
}
```
The Id field data inputed will not be used, so any string input will be valid, but it must be present and cannot be null.

For the class input these is the base data to be used:

```json
{
  "classCode": "string",
  "title": "string",
  "description": "string",
  "students": []
}
```

The classCode and students fields are ignored on the input on the creation, however they are both needed for the structure, handing the classCode like the student id and passing the students as an empty array is enough. For Update Scenarios, the code comparison is made with the object, not the pathfield input, so a valid code is required.

Appart from the basic CRUD functionality of each endpoint the class one also adds and removes students to the internal students array of each class, needing a valid student Id and a vaid classCode.

Every endpoint will return the object of the class/student being handled from the actual json Database, this was done for 2 reasons, first to get the working object in order to validate the response and use it if needed, and guarantee that the database is actually storing and modifing the data on the way that is desired.


### Error Handling

The endpoints work on the base error handling logic used by swagger, meaning that most error scenarios will throw a base response like the following:

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "Students": [
      "The Students field is required."
    ]
  },
  "traceId": "00-1aed7c8ef74de6e7a900b5cb2c6348ff-b222049013d2e07f-00"
}
```

These mostly handle structure and data validation on a functional level, meaning that a logic level error handling is needed, for this purpose some error scenarios were implemented to validate the data inputed and the presence of data, should these scenarios trigger the response will be a single text response like so:

```json
No Valid Student Id found
```

## Running Unit Tests

To run the unit tests you can either change the base project from Presentation to Tests, use the Test console (Test -> Windows -> Test Explorer) , or run them separately by finding the base methods and functions being tried on the managers and running them individually.

A more detailed explanaition on how to run Test projects can be found [here](https://medium.com/@gabrielkerekes/unit-testing-in-c-basics-8493d936dbff).

The Unit Tests are running on xUnit.



