var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.Run(async (HttpContext context) =>
{
    if (context.Request.Method == "GET")
    {

        if (!context.Request.Query.ContainsKey("firstNumber") || !context.Request.Query.ContainsKey("secondNumber") || !context.Request.Query.ContainsKey("operation"))
        {
            context.Response.Headers["Content-type"] = "text/plain";
            context.Response.StatusCode = 400;

            if (!context.Request.Query.ContainsKey("firstNumber"))
            {
                await context.Response.WriteAsync("Invalid input for 'firstNumber'\n");
            }
            if (!context.Request.Query.ContainsKey("secondNumber"))
            {
                await context.Response.WriteAsync("Invalid input for 'secondNumber'\n");
            }
            if (!context.Request.Query.ContainsKey("operation"))
            {
                await context.Response.WriteAsync($"Invalid input for 'operation'\n");
            }
        }
        else
        {
            int FirstNumber;
            int SecondNumber;
            string? Operation = context.Request.Query["operation"];


            if (int.TryParse(context.Request.Query["firstNumber"], out FirstNumber))
            {
                if (int.TryParse(context.Request.Query["secondNumber"], out SecondNumber))
                {
                    context.Response.Headers["Content-type"] = "text/plain";
                    switch (Operation)
                    {
                        case "ADD":
                        case "Add":
                        case "add":
                        case "Addition":
                        case "addition":
                            context.Response.StatusCode = 200;
                            await context.Response.WriteAsync($"{FirstNumber + SecondNumber}");
                            break;
                        case "SUB":
                        case "Sub":
                        case "sub":
                        case "Subtraction":
                        case "subtraction":
                            context.Response.StatusCode = 200;
                            await context.Response.WriteAsync($"{FirstNumber - SecondNumber}");
                            break;
                        case "MULT":
                        case "Mult":
                        case "mult":
                        case "Multiply":
                        case "multiply":
                        case "Multiplication":
                        case "multiplication":
                            context.Response.StatusCode = 200;
                            await context.Response.WriteAsync($"{FirstNumber * SecondNumber}");
                            break;
                        case "DIV":
                        case "Div":
                        case "div":
                        case "Division":
                        case "division":
                            if (SecondNumber > 0)
                            {
                                context.Response.StatusCode = 200;
                                await context.Response.WriteAsync($"{FirstNumber / SecondNumber}");
                            }
                            else
                            {
                                context.Response.StatusCode = 400;
                                await context.Response.WriteAsync("Invalid input for 'secondNumber': Cannot divide by 0\n");
                            }
                            break;
                        default:
                            context.Response.StatusCode = 400;
                            await context.Response.WriteAsync($"Invalid input for 'operation'\n");
                            break;
                        case "Mod":
                        case "MOD":
                        case "mod":
                        case "Module":
                        case "module":
                            if (SecondNumber > 0)
                            {
                                context.Response.StatusCode = 200;
                                await context.Response.WriteAsync($"{FirstNumber % SecondNumber}");
                            }
                            else
                            {
                                context.Response.StatusCode = 400;
                                await context.Response.WriteAsync("Invalid input for 'secondNumber': Cannot divide by 0\n");
                            }
                            break;
                    }
                }
                else
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Invalid input for 'secondNumber'\n");
                }
            }
            else
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid input for 'firstNumber'\n");
            }
        }
    }
    else
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync($"Invalid HTTP method\n");
    }
});

app.Run();
