using LineRunner.Domain.NPCs;

namespace LineRunner.Application.Interfaces;

public interface INpcRepository
{
    Task<Npc?> GetByIdAsync(string id);
    Task UpdateAsync(Npc npc);
}