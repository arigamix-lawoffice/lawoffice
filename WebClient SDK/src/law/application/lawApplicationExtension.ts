import { ApplicationExtension, IApplicationExtensionContext } from "tessa";
import { ComponentRegistry } from "tessa/ui/cards";
import { LawCaseHeaderViewModel } from "../ui/caseHeader/lawCaseHeaderViewModel";
import { LawCaseHeader } from "../ui/caseHeader/lawCaseHeader";

/**
 * Расширение, связанное в жизненным циклом приложения
 */
export class LawApplicationExtension extends ApplicationExtension {
    public initialize(_context: IApplicationExtensionContext): void {
        ComponentRegistry.instance.register(
            LawCaseHeaderViewModel.type,
            (_viewModel: LawCaseHeaderViewModel) => {
                return LawCaseHeader;
            }
        );
    }
}