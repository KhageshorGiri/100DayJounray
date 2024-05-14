using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using TODOgRpcService.Data;
using TODOgRpcService.Models;

namespace TODOgRpcService.Services
{
    public class ToDoItemsService : ToDoItems.ToDoItemsBase
    {
        private readonly gRpcDbcontext _dbContext;
        public ToDoItemsService(gRpcDbcontext context)
        {
            _dbContext = context;
        }

        public override async Task<CreateTodoItemResponse> CreateTodoItem(CreateTodoItemRequese request, ServerCallContext context)
        {
            if (request.ItemName == string.Empty || request.Description == string.Empty)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "You must provide required value."));

            var newTodoItem = new ToDoItem
            {
                ItemName = request.ItemName,
                Description = request.Description,
            };

            _dbContext.ToDoItems.Add(newTodoItem);
            await _dbContext.SaveChangesAsync();

            return await Task.FromResult(new CreateTodoItemResponse
            {
                Id = newTodoItem.Id
            });
        }
      
        public override async Task<ReadToDoResponse> GetTodoItem(ReadToDoRequest request, ServerCallContext context)
        {
            if (request.Id <= 0)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Please provide valid item id."));

            var todoItem = await _dbContext.ToDoItems.FirstOrDefaultAsync(t => t.Id == request.Id);

            if (todoItem is null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Item with Id {request.Id} is not found"));

            return await Task.FromResult(new ReadToDoResponse
            {
                Id = todoItem.Id,
                ItemName = todoItem.ItemName,
                Description = todoItem.Description,
                Status = todoItem.Status
            });
        }

        public override async Task<GetAllResponse> GetAllTodoItems(GetAllRequest request, ServerCallContext context)
        {
            var response = new GetAllResponse();
            var todoItems = await _dbContext.ToDoItems.ToListAsync();
            foreach(var todo in todoItems)
            {
                response.ToDo.Add(new ReadToDoResponse
                {
                    Id = todo.Id,
                    ItemName = todo.ItemName,
                    Description = todo.Description,
                    Status = todo.Status
                });
            }

            return await Task.FromResult(response);
        }


        public override async Task<UpdateToDoResponse> UpdateTodoItem(UpdateToDoRequest request, ServerCallContext context)
        {
            if (request.Id <= 0)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Please provide valid item id."));

            var todoItem = await _dbContext.ToDoItems.FirstOrDefaultAsync(t => t.Id == request.Id);

            if (todoItem is null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Item with Id {request.Id} is not found"));

            todoItem.ItemName = request.ItemName;
            todoItem.Description = request.Description;
            todoItem.Status = request.Status;

            _dbContext.ToDoItems.Update(todoItem);
            await _dbContext.SaveChangesAsync();

            return await Task.FromResult(new UpdateToDoResponse { Id = todoItem.Id });

        }

        public override async Task<DeleteToDoResponse> DeleteTodoItemm(DeleteToDoRequest request, ServerCallContext context)
        {
            if (request.Id <= 0)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Please provide valid item id."));

            var todoItem = await _dbContext.ToDoItems.FirstOrDefaultAsync(t => t.Id == request.Id);

            if (todoItem is null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Item with Id {request.Id} is not found"));

            _dbContext.ToDoItems.Remove(todoItem);
            await _dbContext.SaveChangesAsync();

            return await Task.FromResult(new DeleteToDoResponse { Id = todoItem.Id });

        }
    }
}
