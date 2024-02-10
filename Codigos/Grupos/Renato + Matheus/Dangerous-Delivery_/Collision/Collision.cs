using System.Collections.Generic;
using System.Windows.Forms;

public class Collision
{
    private static readonly Collision current = new Collision();
    public static Collision Current => current;

    public List<ObjBox> ObjectsBox { get; private set; }

    private Collision()
    {
        ObjectsBox = new List<ObjBox>();
    }

    public void AddObjBox(ObjBox boxObject) => ObjectsBox.Add(boxObject);

    public void RemoveObjBox(ObjBox boxObject) => ObjectsBox.Remove(boxObject);

    public bool CheckCollisions(ObjBox obj)
    {
        for (int j = 0; j < ObjectsBox.Count; j++)
        {
            ObjBox other = ObjectsBox[j];

            if (other == obj)
                continue;

            if (CollisionDetected(obj, other) && other is Player)
            {
                return true;
            }
        }
        return false;
    }

    private bool CollisionDetected(ObjBox obj1, ObjBox obj2) => obj2.Box.IntersectsWith(obj1.Box);
}
