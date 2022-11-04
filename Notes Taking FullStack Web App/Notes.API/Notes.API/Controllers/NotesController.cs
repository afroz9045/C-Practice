using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes.API.Data;
using Notes.API.Models.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Notes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly NotesDbContext _notesDbContext;

        public NotesController(NotesDbContext notesDbContext)
        {
            _notesDbContext = notesDbContext;
        }
        [HttpGet]
        public async Task<ActionResult> GetAllNotes()
        {
            // Get the notes from DB
           return Ok(await _notesDbContext.Notes.ToListAsync());

        }

        [HttpGet]
        [Route("{id:Guid}")]
      
        public async Task<ActionResult> GetNoteById([FromRoute]Guid id)
        {
            //await _notesDbContext.Notes.FirstOrDefaultAsync(note=>note.Id==id);
            var note = await _notesDbContext.Notes.FindAsync(id);
            if(note == null)
                return NotFound();
            return Ok(note);
        }

        [HttpPost]
        [ActionName("GetNoteById")]
        public async Task<ActionResult> AddNote(Note note)
        {
            note.Id = Guid.NewGuid();
            await _notesDbContext.Notes.AddAsync(note);
            await _notesDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNoteById), new { id = note.Id }, note);
        }

        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<ActionResult> UpdateNote([FromRoute] Guid id, [FromBody] Note updatedNote)
        {
            var existingNote = await _notesDbContext.Notes.FindAsync(id);
            if (existingNote == null)
                NotFound();
            existingNote.Title = updatedNote.Title;
            existingNote.Description = updatedNote.Description;
            existingNote.IsVisible =  updatedNote.IsVisible;

            await _notesDbContext.SaveChangesAsync();

            return Ok(existingNote);
        }

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<ActionResult> DeleteNote([FromRoute] Guid id)
        {
            var existingNote = await _notesDbContext.Notes.FindAsync(id);

            if (existingNote == null)
                NotFound();
            _notesDbContext.Notes.Remove(existingNote);
            await _notesDbContext.SaveChangesAsync();
            return Ok();

        }

    }
}
