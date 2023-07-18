import { CardControlType, CardControlTypeUsageMode, CardControlTypeFlags } from 'tessa/cards';

// тип создаваемого контрола
export const SliderControlType = new CardControlType(
  'c8bc8df5-c802-480a-9a18-1eab1f1f9e4b', // присваиваем уникальный id
  'Slider', // даем имя
  CardControlTypeUsageMode.Entry, // работает с единичной секцией/строкой
  CardControlTypeFlags.UseEverywhere // можем использовать создаваемый контрол везде (В карточках, в заданиях и тд.)
);
