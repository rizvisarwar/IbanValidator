# IbanValidator

## Different aspects of the code:
###	Mediator pattern
Implemented Mediator pattern, by using MediatR.
###	Validation
Used FluentValidation, to validate input and show useful response texts when any business rule is not satisfied.
###	Mapping
Used AutoMapper in the Controller to show how we could keep the code clean and perform complex object mappings.
###	Web Api documentation
Configured swagger for easily testing the Api.
###	Testing
Added a simple test project to demonstrate how the handler could be tested using xUnit.
 
## Output explanation:
#### Sample input
{
  "iban": "GB82WEST12345698765432"
}

#### Sample output
{
  "isValid": true
}
