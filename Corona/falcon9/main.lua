-----------------------------------------------------------------------------------------
--
-- main.lua
--
-----------------------------------------------------------------------------------------

-- Your code here

local isPushUp =false;
local isPushLeft=false;
local isPushRight = false;
local power=0;

local background = display.newImageRect( "background.png", 360, 570 )
background.x = display.contentCenterX
background.y = display.contentCenterY

local platform = display.newImageRect( "platform.png", 360, 50 )
platform.x = display.contentCenterX
platform.y = display.contentHeight

local sheetOptions =
{
    width = 40,
    height = 175,
    numFrames = 8
}
local sheet_missle = graphics.newImageSheet( "missle.png", sheetOptions )

local sequences_missle = {
    {
        name = "off",
        frames = {7},
        time = 50,
        loopCount = 0
    },
    {
        name = "lowPower",
        frames = {7,5},
        time = 50,
        loopCount = 0
    },
    {
        name = "normalPower",
        frames = {3,1},
        time = 50,
        loopCount = 0
    },
    {
        name = "highPower",
        frames = {8,6},
        time = 50,
        loopCount = 0
    },
    {
        name = "superPower",
        frames = {4,2},
        time = 50,
        loopCount = 0
    }
}

local sprite_missle = display.newSprite( sheet_missle, sequences_missle )

sprite_missle.x=display.contentCenterX
sprite_missle.y=0
sprite_missle.rotation=math.random( -15, 15 )
sprite_missle:setSequence("off")
sprite_missle:play()

local physics = require( "physics" )
physics.start()

physics.addBody( platform, "static", {bounce=0,friction=1} )
physics.addBody( sprite_missle, "dynamic", {bounce=0,friction=1,  shape={-20,-87.5, 20,-87.5, 20,47.5, -20,47.5}} )



local btnLeft = display.newImageRect("button-left.png",50,50);
btnLeft.x=25
btnLeft.y = display.contentHeight

local btnRight = display.newImageRect("button-right.png",50,50);
btnRight.x=display.contentWidth-25
btnRight.y = display.contentHeight

local btnUp = display.newImageRect("button-up.png",50,50);
btnUp.x=display.contentCenterX
btnUp.y = display.contentHeight

local function pushMissleLeft()
    local leftX,leftY =  sprite_missle:localToContent(-1,-80)
    local rightX,rightY =  sprite_missle:localToContent(1,-80)
    sprite_missle:applyLinearImpulse( (leftX-rightX)/2/100, (leftY-rightY)/2/100, leftX, leftY )
end

local function pushMissleRight()
    local leftX,leftY =  sprite_missle:localToContent(-1,-80)
    local rightX,rightY =  sprite_missle:localToContent(1,-80)
    sprite_missle:applyLinearImpulse( (rightX-leftX)/2/100, (rightY-leftY)/2/100, rightX, rightY )
end

local function pushMissleUp()
    local topX,topY =  sprite_missle:localToContent(0,-100)
    local bottomX,bottomY =  sprite_missle:localToContent(0,100)
    sprite_missle:applyLinearImpulse( (topX-bottomX)/175/7, (topY-bottomY)/175/7, bottomX, bottomY )
end

local function onLocalCollision( self, event )
    if (event.force > 0.1 or  sprite_missle.rotation > 7) then
        display.newText( "explosion", display.contentCenterX, display.contentCenterY, native.systemFont, 28 )
        local explosion = display.newImageRect( "explosion.png", 160, 70 )
        explosion.x = sprite_missle.x
        explosion.y = sprite_missle.y        
    end
end


local function myTouchListener( event ) 
    if ( event.phase == "began" ) then
        if(event.target==btnUp) then 
            isPushUp=true
        end
        if(event.target==btnLeft) then 
            isPushLeft=true
        end
        if(event.target==btnRight) then 
            isPushRight=true
        end
    elseif ( event.phase == "ended" ) then
        if(event.target==btnUp) then 
            isPushUp=false
            power=0
            sprite_missle:setSequence("off")
            sprite_missle:play()
        end
        if(event.target==btnLeft) then 
            isPushLeft=false
        end
        if(event.target==btnRight) then 
            isPushRight=false
        end
    end
    return true  -- Prevents tap/touch propagation to underlying objects
end

btnUp:addEventListener( "touch", myTouchListener )
btnLeft:addEventListener( "touch", myTouchListener )
btnRight:addEventListener( "touch", myTouchListener )

sprite_missle.postCollision  = onLocalCollision
sprite_missle:addEventListener( "postCollision" )



local function tick( event )
    if(isPushUp) then 
        pushMissleUp()
        if(power>9) then
            if(sprite_missle.sequence.name~="superPower") then
                sprite_missle:setSequence("superPower")
                sprite_missle:play()
            end
        elseif(power>6) then
            if(sprite_missle.sequence.name~="highPower") then
                sprite_missle:setSequence("highPower")
                sprite_missle:play()
            end
            power=power+1
        elseif(power>3) then
            if(sprite_missle.sequence.name~="normalPower") then
                sprite_missle:setSequence("normalPower")
                sprite_missle:play()
            end
            power=power+1
        elseif(power>0) then
            if(sprite_missle.sequence.name~="lowPower") then
                sprite_missle:setSequence("lowPower")
                sprite_missle:play()                         
            end
            power=power+1
        else
            power=power+1   
        end
    end
    if(isPushRight) then 
        pushMissleRight()
    end    
    if(isPushLeft) then 
        pushMissleLeft()
    end
    timer.performWithDelay( 100, tick )
end

timer.performWithDelay( 100, tick )