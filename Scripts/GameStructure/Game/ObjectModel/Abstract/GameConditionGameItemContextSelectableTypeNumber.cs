﻿//----------------------------------------------
// Flip Web Apps: Game Framework
// Copyright © 2016 Flip Web Apps / Mark Hewitt
//
// Please direct any bugs/comments/suggestions to http://www.flipwebapps.com
// 
// The copyright owner grants to the end user a non-exclusive, worldwide, and perpetual license to this Asset
// to integrate only as incorporated and embedded components of electronic games and interactive media and 
// distribute such electronic game and interactive media. End user may modify Assets. End user may otherwise 
// not reproduce, distribute, sublicense, rent, lease or lend the Assets. It is emphasized that the end 
// user shall not be entitled to distribute or transfer in any way (including, without, limitation by way of 
// sublicense) the Assets in any other way than as integrated components of electronic games and interactive media. 

// The above copyright notice and this permission notice must not be removed from any files.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//----------------------------------------------

using UnityEngine;

namespace GameFramework.GameStructure.Game.ObjectModel.Abstract
{
    /// <summary>
    /// Base GameCondition class that that allows for specifying the GameItem context
    /// </summary>
    /// NOTE: FromLoop mode we need to do in awake so ensure this is setup so we don't support that mode here
    /// jsut add a GameItemContext component and reference that if so needed.
    [System.Serializable]
    public abstract class GameConditionGameItemContextSelectableTypeNumber<T> : GameConditionGameItemContextSelectableType
    {
        /// <summary>
        /// The type of the condition for number types.
        /// </summary>
        public GameConditionHelper.ComparisonType Comparison
        {
            get
            {
                return _comparison;
            }
            set
            {
                _comparison = value;
            }
        }
        [Tooltip("The type of the condition for number types.")]
        [SerializeField]
        GameConditionHelper.ComparisonType _comparison;

        /// <summary>
        /// The type of the condition for float types.
        /// </summary>
        public T Value

        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
        [Tooltip("A reference value to compare the current value against.")]
        [SerializeField]
        T _value;
    }
}
