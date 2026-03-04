using AlunosApi.Models;
using AlunosApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlunosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AlunosController : ControllerBase
    {

        private IAlunoService _alunoService;

        public AlunosController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunos()
        {
            try
            {
                var alunos =await _alunoService.GetAlunos();
                return Ok(alunos);
            }
            catch 
            {
                //return BadRequest("Request invalid");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error (500)");
            }
        }

        [HttpGet("AlunoPorNome")]
        public async Task <ActionResult<IAsyncEnumerable<Aluno>>> GetAlunosBynome([FromQuery]string name)
        {
            try
            {
                var alunos = await _alunoService.GetAlunosByNome(name);
                if (alunos == null)
                    return NotFound($" Does not exist students");
                return Ok(alunos);
            }
            catch {

                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error (500)");
            }
        }

        [HttpGet("{id:int}",Name="GetAluno")]
        public async Task<ActionResult<Aluno>> GetAluno(int id)
        {
            try
            {
                var aluno = await _alunoService.GetAluno(id);

                if (aluno == null)
                    return NotFound($"Does not exist students with id{id}");
                return Ok(aluno);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error (500)");
            }
        }


        [HttpPost]
        public async Task<ActionResult>Create(Aluno aluno)
        {
            try
            {
                await _alunoService.CreateAluno(aluno);
                return CreatedAtRoute(nameof(GetAluno), new { id = aluno.Id }, aluno);

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error (500)");
            }
        }

        [HttpPut("{id:int}")]
        public async Task <ActionResult>Edit(int id, [FromBody] Aluno aluno)
        {

            try
            {
                if(aluno.Id == id)
                {
                    await _alunoService.UpdateAluno(aluno);
                    return Ok($"Students with id={id} was update sucess");
                }
                else
                {
                    return BadRequest("The request failed because of inconsistent data");
                }
                
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error (500)"); 
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {

            try
            {
               var aluno =await _alunoService.GetAluno(id);
                if(aluno != null)
                {
                    await _alunoService.DeleteAluno(aluno);
                    return Ok($" The students with id={id} was delete success");
                }
                else
                {
                    return NotFound($" Students with id={id} do not found");
                }

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error (500)");
            }
        }













    }
}
