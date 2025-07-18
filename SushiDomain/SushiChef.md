```mermaid
sequenceDiagram
    participant SushiChef
    participant SushiFactory
    participant ConveyorBelt
    SushiChef->>SushiChef:OnTimerTick
    SushiChef->>SushiFactory:CreateRandomSushi
    SushiFactory->>SushiChef:sushi    
    loop until sushi on belt
        SushiChef->>ConveyorBelt:TryPutSushiOnBelt
        opt fail
            SushiChef->>SushiChef:sleep N seconds
        end
    end

```
