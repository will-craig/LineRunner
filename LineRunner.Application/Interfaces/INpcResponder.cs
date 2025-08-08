using LineRunner.Domain.NPCs;

namespace LineRunner.Application.Interfaces;

public interface INpcResponder
{
    Task<string> GenerateResponse(Npc npc, string input);
}