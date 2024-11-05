# Feedback for assignment 04.1

Pěkné Unit testy.
Jde je udělat robustněji a mít x dalších, ale za mě to je dostatečné pokrztí Unit testy.
Jenom obecně, je lepší do Assertu nic nehardcodit - máme k dispozici proměnné, nemusíme nic natrvdo psát.

Např. test
```
[Fact]
    public void Create_ValidItem_ReturnsCreatedItem()
    {

        // Arrange
        var controller = new ToDoItemsController();
        var newToDoItem = new ToDoItemCreateRequestDto("New Task", "New Task Description", false);

        // Act
        var result = controller.Create(newToDoItem);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var item = Assert.IsType<ToDoItemGetResponseDto>(createdResult.Value);
        Assert.Equal("New Task", item.Name);
        Assert.Equal("New Task Description", item.Description);
        Assert.False(item.IsCompleted);
    }

 ```
bychom mohli parametrizovat - parametrizovatelne bylo
```
var newToDoItem = new ToDoItemCreateRequestDto(parametr1, parametr2, parametr3);
```
a test by se nám tím rozbil jelikož máme natrvdo dané v Assertech false, "New Task", "New Task Description".

Lepší je mít

```
[Fact]
    public void Create_ValidItem_ReturnsCreatedItem()
    {

        // Arrange
        var controller = new ToDoItemsController();
        var newToDoItem = new ToDoItemCreateRequestDto("New Task", "New Task Description", false);

        // Act
        var result = controller.Create(newToDoItem);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var item = Assert.IsType<ToDoItemGetResponseDto>(createdResult.Value);
        Assert.Equal(newToDoItem.Name, item.Name);
        Assert.Equal(newToDoItem.Description, item.Description);
        Assert.Equal(newToDoItem.IsCompleted, item.IsCompleted);
    }

 ```
