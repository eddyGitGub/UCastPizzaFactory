# UCastPizzaFactory
This pizza factory application can cook pizza with the following base and topping 
Pizza Base	Topping
Deep Pan	Ham and Mushroom
Stuffed Crust	Pepperoni
Thin and Crispy	Vegetable

The cooking time depends on the type of pizza being cook.
Already cooked pizza type cannot be cook again (duplicate not allow)
At the start of the application 50 pizza can be produce (only nine unique pizza type is possible from the available materials). 
Here are instruction to cook a pizza
For Piza Base enter 1: for Deep Pan, 2: for Stuffed Crust and 3: for Thin and Crispy
For Pizza Topping enter 1: for Ham and Mushroom, 2:Pepperoni and 3:for Vegetable
And the pizza will ready depending on the pizza type.
Note: is not possible to cook a pizza without any topping.
Design Decision.
Asynchronous methods were used for method implementation because of the I/O-bound (saving record to a file) and to enable multiple related operations to run concurrently which will improve the application's performance and responsiveness.

Some element SOLID principle were implemented like dependency injection for decoupling, easy code maintainability, clean code and unit testing (if need be)
