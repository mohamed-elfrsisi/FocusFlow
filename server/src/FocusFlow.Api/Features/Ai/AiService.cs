// File      : server/src/FocusFlow.Api/Features/Ai/AiService.cs
// Developer : Yasmeen Ayman (M3)
// Reviewer  : Saeid El-Kholy (M4)
// Task(s)   : T056, T057, T066  -  Retrieval
// Module    : FR-06 (FSD §9)
// Purpose   : Prompt builder (history, numbered sources), Gemini first then Groq fallback, daily limit 30, error codes AI_LIMIT_REACHED / AI_UNAVAILABLE.
