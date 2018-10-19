namespace Rias.Domain

module EventBox =
    let boxOne (state: StateBox<'state>) (command: CommandBox<'command>) (event: 'event) =
        { Version = state.Version + 1; StreamId = command.StreamId; Event = event }
        |> List.singleton
