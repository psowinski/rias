namespace Rias.Domain

[<AutoOpen>]
module StreamIdentity =

    type StreamName = string
    type StreamUniqueId = string
    type StreamId = private StreamId of StreamName * StreamUniqueId

    module StreamId =
        open System
        let create name id = 
            if String.IsNullOrWhiteSpace name then Error "Invalid name."
            elif String.IsNullOrWhiteSpace id then Error "Invalid id."
            else StreamId (name, id) |> Ok

        let toString (StreamId (name, id)) = name + "-" + id

        let parse (x: string) = 
            let error () = Error "Invalid input argument."
            let parts = x.Split('-')
            if parts.Length <> 2 then error ()
            else 
                match Array.tryFind String.IsNullOrWhiteSpace parts with
                | None -> StreamId (parts.[0], parts.[1]) |> Ok
                | _ -> error ()
