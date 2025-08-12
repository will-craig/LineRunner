using LineRunner.Domain.NPCs;

namespace LineRunner.Domain.Interfaces;

public interface INpcResponder
{
    Task<string> GenerateResponse(Npc npc, string input);
}