using System.Collections;
using System.Collections.Generic;
using Code.Infrastructure.AssetManagement;
using Code.StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Code.Wheel
{
    [RequireComponent(typeof(ISpinWheel), typeof(Win))]
    public class WheelFiller : MonoBehaviour
    {
        [SerializeField] private Transform _sectorContainer;
        [SerializeField] private float _secondsToRefresh;

        public Transform SectorContainer => _sectorContainer;

        private List<GameObject> _sectors = new List<GameObject>();
        private List<Image> _prizesIcons = new List<Image>();
        private List<TextMeshProUGUI> _prizesLabels = new List<TextMeshProUGUI>();
        private IAssets _assets;
        private ISpinWheel _spinWheel;
        private Win _win;

        public void Initialize(IAssets assets)
        {
            _spinWheel = GetComponent<ISpinWheel>();
            _win = GetComponent<Win>();
            _assets = assets;
            _spinWheel.OnSpinEnded += Refresh;
        }

        private void OnDestroy()
        {
            if (_spinWheel != null)
                _spinWheel.OnSpinEnded -= Refresh;
        }

        public void FillSectors(Transform sectorContainer, IReadOnlyList<PrizeStaticData> randomPrizes)
        {
            for (int i = 0; i < randomPrizes.Count; i++)
            {
                GameObject sector = _assets.Instantiate(AssetPath.SectorPath, sectorContainer);
                float hue = (float)i / randomPrizes.Count;
                float angle = i * 360.0f / randomPrizes.Count;
                var image = sector.GetComponent<Image>();
                image.color = Color.HSVToRGB(hue, 1, 1);
                image.fillAmount = 1.0f / randomPrizes.Count;
                sector.transform.rotation = Quaternion.Euler(0, 0, angle);

                _sectors.Add(sector);

                SetSectorInfo(sector.transform, image.fillAmount, randomPrizes[i]);
            }
        }

        private void Refresh()
        {
            StartCoroutine(WaitBeforeRefresh());
        }

        private IEnumerator WaitBeforeRefresh()
        {
            yield return new WaitForSeconds(_secondsToRefresh);
            UpdateImagesAndLabels();
        }

        private void UpdateImagesAndLabels()
        {
            for (int i = 0; i < _win.PrizesCount; i++)
            {
                _prizesIcons[i].sprite = _win.Prizes[i].Prefab.GetComponent<Image>().sprite;
                if (_win.Prizes[i].Amount > 1 && !_win.Prizes[i].IsUnique)
                {
                    _prizesLabels[i].text = _win.Prizes[i].Amount.ToString();
                    _prizesLabels[i].gameObject.SetActive(true);
                }
                else
                {
                    _prizesLabels[i].gameObject.SetActive(false);
                }
            }
        }

        private void SetSectorInfo(Transform sector, float fillAngle, PrizeStaticData prizeData)
        {
            var radians = (fillAngle - fillAngle / 2.0f) * 2.0f * Mathf.PI;
            var delta = Mathf.Sqrt(1000f * fillAngle);
            float radius = (sector.GetComponent<RectTransform>().sizeDelta.x) / 2.0f - delta;

            float x = radius * Mathf.Cos(radians);
            float y = radius * Mathf.Sin(radians);

            var prizeIcon = Object.Instantiate(prizeData.Prefab, sector);
            prizeIcon.transform.localPosition = new Vector3(-x, y, 0);
            prizeIcon.transform.localRotation = Quaternion.Euler(0, 0, 90 - radians * 180.0f / Mathf.PI);

            _prizesIcons.Add(prizeIcon.GetComponent<Image>());

            SetItemText(sector, prizeData, radius, radians);
        }

        private void SetItemText(Transform sector, PrizeStaticData prizeData, float radius, float radians)
        {
            float delta = 0.65f * radius;
            float x = delta * Mathf.Cos(radians);
            float y = delta * Mathf.Sin(radians);

            var rect = new GameObject("[Text]").AddComponent<RectTransform>();
            rect.transform.SetParent(sector);
            rect.transform.localPosition = new Vector3(-x, y);
            rect.transform.localRotation = Quaternion.Euler(0, 0, 90 - radians * 180.0f / Mathf.PI);
            rect.transform.localScale = Vector3.one;

            var text = rect.gameObject.AddComponent<TextMeshProUGUI>();
            text.fontSize = 7f;
            text.enableWordWrapping = false;
            text.alignment = TextAlignmentOptions.Center;

            _prizesLabels.Add(text);

            if (prizeData.Amount > 1 && !prizeData.IsUnique)
            {
                text.text = prizeData.Amount.ToString();
                text.gameObject.SetActive(true);
            }
            else
            {
                text.gameObject.SetActive(false);
            }
        }
    }
}
