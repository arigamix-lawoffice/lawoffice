import { FC } from 'react';
import { UIButton } from 'tessa/ui';
import { TextFieldProps } from './textField';
interface TextFieldWithButtonsProps extends TextFieldProps {
    buttons: UIButton[];
}
declare const TextFieldWithButtons: FC<TextFieldWithButtonsProps>;
export default TextFieldWithButtons;
