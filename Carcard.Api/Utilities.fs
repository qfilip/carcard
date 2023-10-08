module Utilities
    module Option =
        let defaultList (mapper: 'a -> 'b) (x: option<list<'a>>) =
            x
            |> Option.map (List.map mapper)
            |> Option.defaultWith (fun _ -> [])