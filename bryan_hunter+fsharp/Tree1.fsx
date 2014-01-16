open System
open System.Drawing
open System.Windows.Forms

// Create a form to display the graphics
let width, height = 800, 600         
let form = new Form(Width = width, Height = height)
let box = new PictureBox(BackColor = Color.White, Dock = DockStyle.Fill)
let image = new Bitmap(width, height)
let graphics = Graphics.FromImage(image)
let brush = new SolidBrush(Color.FromArgb(0, 128, 0))

box.Image <- image
form.Controls.Add(box) 

// Compute the endpoint of a line
// starting at x, y, going at a certain angle
// for a certain length. 
let endpoint x y angle length =
    x + length * cos angle,
    y + length * sin angle

let flip x = (float)height - x

// Utility function: draw a line of given width, 
// starting from x, y
// going at a certain angle, for a certain length.
let drawLine (target : Graphics) (brush : Brush) 
             (x : float) (y : float) 
             (angle : float) (length : float) (width : float) =
    let x_end, y_end = endpoint x y angle length
    let origin = new PointF((single)x, (single)(y |> flip))
    let destination = new PointF((single)x_end, (single)(y_end |> flip))
    let pen = new Pen(brush, (single)width)
    target.DrawLine(pen, origin, destination)

let draw x y angle length width = 
    drawLine graphics brush x y angle length width

let pi = Math.PI

// Now... your turn to draw
// The trunk

let rec drawNext (currentDepth:int) (x:float) (y:float) (angle:float) 
    (length:float) (thickness:float) =
    match currentDepth with
       | 0 -> ()
       | _ -> 
            let stillToGo = currentDepth - 1

            for i in -1 .. 1 do
                let newAngle = angle + (float i) / 3. 
                let nextThickness = thickness * 0.5

                let nextLength = length * 0.85 
            
                let nextX, nextY = endpoint x y angle length
                draw nextX nextY newAngle nextLength nextThickness
                drawNext stillToGo nextX nextY newAngle nextLength nextThickness


drawNext 8 ((float width) / 2.) -50. (pi*(0.5)) 120. 10.



form.ShowDialog()