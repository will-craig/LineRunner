# LineRunner
AI powered NPC dialogue generation

Work in progress...

Plan is to a create a GraphQL api that can be queried via a client/game. And a kafka stream of in game events to keep npc knowlege up todate for prompts. Then driving the dialogue prompt will be a gpt llm throuh azure-openapi

Components
1. GraphQL API (Query + Mutation Layer)
   
-Acts as the main interface for the game client.

-Used to query available NPCs, locations, or dialogue state.

-Used to send prompts (e.g., TalkToNpc) and receive AI-generated responses.

3. Kafka (Event Stream)
   
-Used for sending and processing in-game events (e.g., player actions, world changes).

-These events update NPCs' contextual knowledge.

-Will use a vector db to persist npc memory

5. Azure AI (OpenAI GPT-4 / Chat Completions)
   
Powers the generation of NPC dialogue responses.

Consumes prompt context based on:

  - Recent events

  - NPC knowledge base
    
  - Current conversation thread

Will need to use Azure’s responsible AI tooling, such as prompt filters, logging, or grounding.
