namespace Sandervanteinde.AdventOfCode2021.Solutions._2021;

internal partial class Day18
{
    public class SnailfishPair : SnailfishBase
    {
        private SnailfishBase left;
        private SnailfishBase right;

        public SnailfishBase Left
        {
            get => left;
            set
            {
                left = value;
                left.Parent = this;
            }
        }
        public SnailfishBase Right
        {
            get => right;
            set
            {
                right = value;
                right.Parent = this;
            }
        }

        public override SnailfishBase Clone()
        {
            return new SnailfishPair
            {
                Left = left.Clone(),
                Right = right.Clone()
            };
        }

        public override bool AttemptExplode(int depth = 1)
        {
            if (depth == 5)
            {
                if (Parent == null)
                {
                    throw new InvalidOperationException("Invalid parent");
                }

                var leftValue = left is Snailfish leftFish ? leftFish.Value : throw new InvalidOperationException("Expected left fish at explode to be a value");
                var rightValue = right is Snailfish rightFish ? rightFish.Value : throw new InvalidOperationException("Expected left fish at explode to be a value");
                var closestLeft = FindClosestLeft();
                if (closestLeft is not null)
                {
                    closestLeft.Value += leftValue;
                }
                var closestRight = FindClosestRight();
                if (closestRight is not null)
                {
                    closestRight.Value += rightValue;
                }
                if (Parent.left == this)
                {
                    Parent.Left = new Snailfish(0);
                    return true;
                }
                if (Parent.right == this)
                {
                    Parent.Right = new Snailfish(0);
                    return true;
                }
                throw new InvalidOperationException("Invalid parent");
            }
            return left.AttemptExplode(depth + 1) || right.AttemptExplode(depth + 1);
        }

        public override bool AttemptSplit()
        {
            return left.AttemptSplit() || right.AttemptSplit();
        }

        public Snailfish? FindClosestLeft()
        {
            var parent = Parent;
            var child = this;
            while (parent is not null)
            {
                if (parent.left != child)
                {
                    var rootToGoRight = parent.left;
                    while (rootToGoRight is not Snailfish)
                    {
                        rootToGoRight = ((SnailfishPair)rootToGoRight).right;
                    }
                    return (Snailfish)rootToGoRight;
                }

                child = parent;
                parent = parent.Parent;
            }

            return null;
        }

        public bool IsHierarchyIntact()
        {
            var isIntact = Left.Parent == this && Right.Parent == this &&
                (Left is not SnailfishPair leftPair || leftPair.IsHierarchyIntact()) &&
                (Right is not SnailfishPair rightPair || rightPair.IsHierarchyIntact());
            return isIntact;
        }

        public Snailfish? FindClosestRight()
        {
            var parent = Parent;
            var child = this;
            while (parent is not null)
            {
                if (parent.right != child)
                {
                    var rootToGoLeft = parent.right;
                    while (rootToGoLeft is not Snailfish)
                    {
                        rootToGoLeft = ((SnailfishPair)rootToGoLeft).left;
                    }
                    return (Snailfish)rootToGoLeft;
                }

                child = parent;
                parent = parent.Parent;
            }

            return null;
        }

        public override long Magnitude()
        {
            return 3 * Left.Magnitude() + 2 * Right.Magnitude();
        }

        public override string ToString()
        {
            return $"[{Left},{Right}]";
        }
    }
}
