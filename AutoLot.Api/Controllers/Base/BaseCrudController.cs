namespace AutoLot.Api.Controllers.Base;

[ApiController]
[Route("api/[controller]")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize]
public abstract class BaseCrudController<TEntity, TController> : ControllerBase
    where TEntity : BaseEntity, new()
    where TController : class
{
    protected readonly IAppLogging<TController> Logger;
    protected readonly IBaseRepo<TEntity> MainRepo;

    protected BaseCrudController(IAppLogging<TController> logger, IBaseRepo<TEntity> mainRepo)
    {
        Logger = logger;
        MainRepo = mainRepo;
    }

    /// <summary>
    /// Retrieves all entities of type <typeparamref name="TEntity"/>.
    /// </summary>
    /// <remarks>This method is deprecated and should not be used in new implementations. It is intended for
    /// legacy support only. The method returns a collection of entities if the request is successful, or an appropriate
    /// error response if the request fails.</remarks>
    /// <returns>A collection of entities of type <typeparamref name="TEntity"/> if the request is successful.</returns>
    /// <exception cref="Exception">Always thrown to indicate that this method is not intended for use.</exception>
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(200, "The execution was successful")]
    [SwaggerResponse(400, "The request was invalid")]
    [SwaggerResponse(401, "Unauthorized access attempted")]
    [ApiVersion("0.5", Deprecated = true)]
    [HttpGet]
    public ActionResult<IEnumerable<TEntity>> GetAllBad()
    {
        throw new Exception("I said not to use this one");
    }


    /// <summary>
    /// Retrieves all future entities.
    /// </summary>
    /// <remarks>This method returns a collection of entities that are scheduled for future use or events. The
    /// response is formatted as JSON and adheres to API version 2.0-Beta.</remarks>
    /// <returns>A collection of future entities as an <see cref="IEnumerable{TEntity}"/>. If no entities are found, an empty
    /// collection is returned.</returns>
    /// <exception cref="NotImplementedException">This method is not yet implemented.</exception>
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(200, "The execution was successful")]
    [SwaggerResponse(400, "The request was invalid")]
    [SwaggerResponse(401, "Unauthorized access attempted access attempted")]
    [ApiVersion("2.0-Beta")]
    [HttpGet]
    public ActionResult<IEnumerable<TEntity>> GetAllFuture()
    {
        throw new NotImplementedException("I'm working on it");
    }


    /// <summary>
    /// Retrieves all entities of type <typeparamref name="TEntity"/> from the repository.
    /// </summary>
    /// <remarks>This method returns all entities, bypassing any query filters that may be applied. Use this
    /// method with caution if query filters are intended to enforce security or data visibility.</remarks>
    /// <returns>An <see cref="IEnumerable{T}"/> containing all entities of type <typeparamref name="TEntity"/>. Returns an empty
    /// collection if no entities are found.</returns>
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(200, "The execution was successful")]
    [SwaggerResponse(400, "The request was invalid")]
    [SwaggerResponse(401, "Unauthorized access attempted")]
    [ApiVersion("1.0")]
    [HttpGet]
    public ActionResult<IEnumerable<TEntity>> GetAll()
    {
        return Ok(MainRepo.GetAllIgnoreQueryFilters());
    }

    /// <summary>
    /// Retrieves a single entity by its unique identifier.
    /// </summary>
    /// <remarks>This method returns a 200 OK response with the entity if it is found, a 204 No Content
    /// response if no entity exists with the specified identifier, a 400 Bad Request response if the request is
    /// invalid, or a 401 Unauthorized response if the caller is not authorized.</remarks>
    /// <param name="id">The unique identifier of the entity to retrieve. Must be a positive integer.</param>
    /// <returns>An <see cref="ActionResult{TEntity}"/> containing the requested entity if found. Returns <see langword="null"/>
    /// if no entity exists with the specified identifier.</returns>
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(200, "The execution was successful")]
    [SwaggerResponse(204, "No content")]
    [SwaggerResponse(400, "The request was invalid")]
    [SwaggerResponse(401, "Unauthorized access attempted")]
    [ApiVersion("1.0")]
    [HttpGet("{id:int}")]
    public ActionResult<TEntity> GetOne(int id)
    {
        var entity = MainRepo.Find(id);
        if (entity == null)
        {
            return NoContent();
        }
        return Ok(entity);
    }

    /// <summary>
    /// Updates an existing entity in the repository.
    /// </summary>
    /// <remarks>This method validates that the <paramref name="id"/> matches the <see cref="TEntity.Id"/>
    /// property of the provided <paramref name="entity"/> and that the model state is valid. If validation fails, a
    /// <see cref="StatusCodes.Status400BadRequest"/> response is returned. Exceptions are caught and handled
    /// gracefully, returning a <see cref="StatusCodes.Status400BadRequest"/> response.
    /// Sample body:
    /// <pre>
    /// {
    ///     "Id": 1,
    ///     "TimeStamp": "AAAAAAAAB+E="
    ///     "MakeId": 1,
    ///     "Color": "Black",
    ///     "PetName": "Zippy",
    ///     "MakeColor": "VW (Black)",
    /// }
    /// </pre>
    /// </remarks>
    /// <param name="id">The unique identifier of the entity to update. Must match the <see cref="TEntity.Id"/> property of the provided
    /// <paramref name="entity"/>.</param>
    /// <param name="entity">The updated entity data. The <see cref="TEntity.Id"/> property must match the <paramref name="id"/> parameter.</param>
    /// <returns>An <see cref="IActionResult"/> indicating the result of the operation: <list type="bullet">
    /// <item><description><see cref="StatusCodes.Status200OK"/> if the update was successful.</description></item>
    /// <item><description><see cref="StatusCodes.Status400BadRequest"/> if the <paramref name="id"/> does not match the
    /// <see cref="TEntity.Id"/> property, the model state is invalid, or an exception occurs.</description></item>
    /// <item><description><see cref="StatusCodes.Status401Unauthorized"/> if the caller is not authorized to perform
    /// the operation.</description></item> </list></returns>
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(200, "The execution was successful")]
    [SwaggerResponse(400, "The request was invalid")]
    [SwaggerResponse(401, "Unauthorized access attempted")]
    [HttpPut("{id:int}")]
    [ApiVersion("1.0")]
    public IActionResult UpdateOne(int id, TEntity entity)
    {
        if (id != entity.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        try
        {
            MainRepo.Update(entity);
        }
        catch (CustomException ex)
        {
            //This shows an example with the custom exception
            //Should handle more gracefully
            return BadRequest(ex);
        }
        catch (Exception ex)
        {
            //Should handle more gracefully
            return BadRequest(ex);
        }
        return Ok(entity);
    }


    /// <summary>
    /// Adds a new entity to the repository and returns the created entity.
    /// </summary>
    /// <remarks>This method validates the provided entity before adding it to the repository. If validation
    /// fails,  a 400 Bad Request response is returned. Upon successful addition, the method returns a 201 Created 
    /// response with the location of the newly created entity.
    /// Sample body:
    /// <pre>
    /// {
    ///     "Id": 1,
    ///     "TimeStamp": "AAAAAAAAB+E="
    ///     "MakeId": 1,
    ///     "Color": "Black",
    ///     "PetName": "Zippy",
    ///     "MakeColor": "VW (Black)",
    /// }
    /// </pre>
    /// </remarks>
    /// <param name="entity">The entity to be added. Must not be null and must satisfy validation requirements.</param>
    /// <returns>An <see cref="ActionResult{TEntity}"/> containing the created entity if the operation is successful. Returns a
    /// 400 Bad Request response if the entity is invalid or an error occurs during processing. Returns a 401
    /// Unauthorized response if the caller is not authorized.</returns>
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(201, "The execution was successful")]
    [SwaggerResponse(400, "The request was invalid")]
    [SwaggerResponse(401, "Unauthorized access attempted")]
    [HttpPost]
    [ApiVersion("1.0")]
    public ActionResult<TEntity> AddOne(TEntity entity)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        try
        {
            MainRepo.Add(entity);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }

        return CreatedAtAction(nameof(GetOne), new { id = entity.Id }, entity);
    }



    /// <summary>
    /// Deletes a single record
    /// </summary>
    /// <remarks>
    /// Sample body:
    /// <pre>
    /// {
    ///     "Id": 1,
    ///     "TimeStamp": "AAAAAAAAB+E="
    /// }
    /// </pre>
    /// </remarks>
    /// <returns>Nothing</returns>
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(200, "The execution was successful")]
    [SwaggerResponse(400, "The request was invalid")]
    [SwaggerResponse(401, "Unauthorized access attempted")]
    [HttpDelete("{id}")]
    [ApiVersion("1.0")]
    public ActionResult<TEntity> DeleteOne(int id, TEntity entity)
    {
        if (id != entity.Id)
        {
            return BadRequest();
        }

        try
        {
            MainRepo.Delete(entity);
        }
        catch (Exception ex)
        {
            //Should handle more gracefully
            return new BadRequestObjectResult(ex.GetBaseException()?.Message);
        }

        return Ok();
    }
}
