using LineRunner.Domain.NPCs;

namespace LineRunner.Domain.Interfaces;

public interface INpcRepository
{
    Task<Npc?> GetByIdAsync(string id);
    Task UpdateAsync(Npc npc);
}