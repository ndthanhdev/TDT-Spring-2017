-----------------------------------------------------------------------------------------
--
-- main.lua
--
-----------------------------------------------------------------------------------------

-- Your code here

local isPushUp =false;
local isPushLeft=false;
local isPushRight = false;

local background = display.newImageRect( "background.png", 360, 570 )
background.x = display.contentCenterX
background.y = display.contentCenterY

local platform = display.newImageRect( "platform.png", 360, 50 )
platform.x = display.contentCenterX
platform.y = display.contentHeight

local missle = display.newImageRect( "falcon9.png", 21, 200 )
missle.x = display.contentCenterX
missle.y = 0
missle.rotation=math.random( -15,15 )

local btnLeft = display.newImageRect("button-left.png",50,50);
btnLeft.x=25
btnLeft.y = display.contentHeight

local btnRight = display.newImageRect("button-right.png",50,50);
btnRight.x=display.contentWidth-25
btnRight.y = display.contentHeight

local btnUp = display.newImageRect("button-up.png",50,50);
btnUp.x=display.contentCenterX
btnUp.y = display.contentHeight

local physics = require( "physics" )
physics.start()

physics.addBody( platform, "static", {bounce=0,friction=1} )
physics.addBody( missle, "dynamic", {bounce=0,friction=1,  box={halfWidth=10, halfHeight =100, angle=0}} )

local function pushMissleLeft()
    local leftX,leftY =  missle:localToContent(-10,-75)
    local rightX,rightY =  missle:localToContent(10,-75)
    missle:applyLinearImpulse( (leftX-rightX)/13000, (leftY-rightY)/13000, leftX, leftY )
end

local function pushMissleRight()
    local leftX,leftY =  missle:localToContent(-10,-75)
    local rightX,rightY =  missle:localToContent(10,-75)
    missle:applyLinearImpulse( (rightX-leftX)/13000, (rightY-leftY)/13000, leftX, leftY )
end

local function pushMissleUp()
    local topX,topY =  missle:localToContent(0,-100)
    local bottomX,bottomY =  missle:localToContent(0,100)
    print(topX,topY,bottomX,bottomY)
    missle:applyLinearImpulse( (topX-bottomX)/13000, (topY-bottomY)/13000, bottomX, bottomY )
end

local function onLocalCollision( self, event )
    if (event.force > 0.2 or  missle.rotation > 10) then
        display.newText( "explosion", display.contentCenterX, display.contentCenterY, native.systemFont, 28 )
        local explosion = display.newImageRect( "explosion.png", 160, 70 )
        explosion.x = missle.x
        explosion.y = missle.y        
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

missle.postCollision  = onLocalCollision
missle:addEventListener( "postCollision" )



local function tick( event )
    if(isPushUp) then 
        pushMissleUp()
    end
    if(isPushRight) then 
        pushMissleRight()
    end    
    if(isPushLeft) then 
        pushMissleLeft()
    end
    timer.performWithDelay( 10, tick )
end

timer.performWithDelay( 10, tick )