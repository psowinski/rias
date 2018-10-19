namespace Rias.Persistence

module InMemory =
    open Rias.Domain

    let create<'event> () =
        let collection = ResizeArray<EventBox<'event>>()
        
        let store events = events |> Seq.iter collection.Add |> Ok
        let load streamId = collection |> Seq.filter (fun e -> e.StreamId = streamId)
                                       |> Seq.sortBy (fun e -> e.Version)
                                       |> Ok

        let storage = {
            Store = store
            Load = load
        }
        storage
